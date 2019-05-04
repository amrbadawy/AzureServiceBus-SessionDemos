using Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus.Management;
using System.Threading;

namespace ChatGroupApp
{
    class ChatRoom
    {
        #region • Initialize •

        public string MyUsername { get; }
        public string Name { get; }
        private readonly MessageSender _messageSender;
        private readonly IChatMessageManager _chatMessageManager;
        public ChatRoom(IChatMessageManager chatMessageManager, string name, string username)
        {
            Guard.IsEmpty(username);
            Guard.IsEmpty(name);

            MyUsername = username;
            Name = name;
            _chatMessageManager = chatMessageManager;
            _messageSender = new MessageSender(WorkshopConfig.SBConnectionString, Name);
        }

        #endregion

        private async Task IsEnabled()
        {
            var isDisabled = (await Topic.MgmtClient.GetTopicAsync(Name)).Status == EntityStatus.Disabled;
            if (isDisabled)
                throw new ChatException("Chat is Disabled");
        }

        public async Task Start()
        {
            // 0. check if chat is enabled
            await IsEnabled();

            // 1. Create Subscription for user
            await Subscription
                .CreateSubscriptionIfNotExists(Name, MyUsername)
                .ConfigureAwait(false);

            // 2. Register Message Hnadler
            StartReceiving();
        }

        public async Task Send(string data)
        {
            if (data.IsNullOrWhiteSpace()) return;

            data = Encryption.TryEncrypt(data);

            var message = new Message
            {
                Body = data.ToBytes(),
                //TimeToLive = TimeSpan.FromSeconds(1000),
                Label = MyUsername.ToLower()
            };

            try
            {
                await _messageSender.SendAsync(message);
            }
            catch (MessagingEntityDisabledException)
            {
                throw new ChatException("Chat is Disabled");
            }
        }

        private void StartReceiving()
        {
            var entityPath = EntityNameHelper.FormatSubscriptionPath(Name, MyUsername);
            var receiver = new MessageReceiver(WorkshopConfig.SBConnectionString, entityPath, ReceiveMode.ReceiveAndDelete);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = true
            };
            receiver.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            _chatMessageManager.Show(message);
            await Task.CompletedTask;
        }
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            if (args.Exception.GetType().Name == nameof(MessagingEntityDisabledException))
            {
                _chatMessageManager.Show($"Chat is Disabled.");
            }
            else
            {
                _chatMessageManager.Show($"Error » {args.Exception}.");
            }

            /*var context = args.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");*/
            return Task.CompletedTask;
        }

        #region • Users •

        public async Task<List<string>> LoadUsers()
        {
            var allSubs = await Topic.MgmtClient.GetSubscriptionsAsync(Name);
            return allSubs
                .Select(sub => sub.SubscriptionName.ToLower())
                .Where(name => name != MyUsername.ToLower())
                .ToList();
        }
        public async Task BlockUsers(IEnumerable<string> users)
        {
            if (!users.Any())
                return;

            string sqlExpression = null;
            foreach (var user in users)
            {
                if (sqlExpression == null)
                    sqlExpression += $"sys.Label <> '{user}'";
                else
                    sqlExpression += $" AND sys.Label <> '{user}'";
            }

            var blockUsersRule = new RuleDescription
            {
                Name = "BlockUsers_SqlFilter",
                Filter = new SqlFilter(sqlExpression)
            };

            await Subscription.AddRule(Name, MyUsername, blockUsersRule);
        }
        public async Task UnblockAllUsers()
        {
            var defaultRule = new RuleDescription
            {
                Name = RuleDescription.DefaultRuleName,
                Filter = new TrueFilter()
            };
            await Subscription.AddRule(Name, MyUsername, defaultRule);
        } 

        #endregion
    }
}
