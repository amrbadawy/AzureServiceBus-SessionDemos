using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Threading;
using static ChatGroupApp.WorkshopConfig;

namespace ChatGroupApp
{
    internal static class MessagePump
    {
        public static void Start(MessageReceiver messageReceiver)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = true
            };
            messageReceiver.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var time = message.SystemProperties.EnqueuedTimeUtc.ToLocalTime().ToString("hh:mm:ss");
            var username = message.Label;
            var sequenceNumber = message.SystemProperties.SequenceNumber;
            var text = Encoding.UTF8.GetString(message.Body);

            text = Encryption.TryDecrypt(text, username);

            ShowMessage($"{time} - {username} » {text} #({sequenceNumber})");

            await Task.CompletedTask;
        }
        private static void ShowMessage(string chatMessage)
        {
            ChatArea.Invoke(ctrl => ctrl.Items.Add(chatMessage));
            ChatArea.Invoke(ctrl => ctrl.SelectedIndex = ctrl.Items.Count - 1);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            if (args.Exception.GetType().Name == nameof(MessagingEntityDisabledException))
            {
                ShowMessage($"Chat is Disabled.");
            }
            else
            {
                ShowMessage($"Error » {args.Exception}.");
            }

            /*var context = args.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");*/
            return Task.CompletedTask;
        }
    }
}
