using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Management
{
    partial class Program
    {
        public static async Task<SubscriptionDescription> CreateSubscriptionIfNotExists(string topicName, 
            string subName, RuleDescription ruleDescription = null)
        {
            try
            {
                return await _mgmtClient.GetSubscriptionAsync(topicName, subName);
            }
            catch (MessagingEntityNotFoundException)
            {
                var subDesc = new SubscriptionDescription(topicName, subName)
                {
                    // The maximum delivery count of a message before it is dead-lettered
                    // default value = 10
                    // Min value = 1
                    // Max value = int.Max
                    MaxDeliveryCount = 5,

                    // default value = 60 seconds.
                    // Max value = 5 minutes.
                    LockDuration = TimeSpan.FromSeconds(45),


                    // whether the queue supports the concept of session.
                    RequiresSession = false,

                    // >> Message >>
                    // ExpiresAtUtc = EnqueuedTimeUtc + TimeToLive
                    DefaultMessageTimeToLive = TimeSpan.FromDays(7), // Expiration

                    // Duration of idle interval after which the queue is automatically deleted. 
                    // Default / Max value = TimeSpan.MaxValue >> 29K years
                    // Min value = 5 minute
                    AutoDeleteOnIdle = TimeSpan.MaxValue,

                    // Decides whether an expired message due to TTL should be dead-letterd
                    EnableDeadLetteringOnMessageExpiration = true,
                    EnableBatchedOperations = false
                };

                await _mgmtClient.CreateSubscriptionAsync(subDesc, ruleDescription);

                //await AddRule(topicName, subName, ruleDescription);
                return subDesc;
            }
        }
        public static async Task AddRule(string topicName, string subName, RuleDescription ruleDescription)
        {
            if (ruleDescription == null)
                return;

            var subClient = new SubscriptionClient(SBConnectionString, topicName, subName);
            
            // remove any old rules
            foreach (var rule in await subClient.GetRulesAsync())
            {
                await subClient.RemoveRuleAsync(rule.Name);
            }
            // add rule
            await subClient.AddRuleAsync(ruleDescription);
        }

        private static async Task UpdateSubscription(SubscriptionDescription description)
        {
            description.DefaultMessageTimeToLive = TimeSpan.FromDays(6);

            await _mgmtClient.UpdateSubscriptionAsync(description);
        }

        private static async Task GetSubscriptionRuntimeInfo(string topicName, string subName)
        {
            var runtimeInfo = await _mgmtClient.GetSubscriptionRuntimeInfoAsync(topicName, subName);
            Console.WriteLine($"Retrieved runtime information \n " +
                $"Active messages:{runtimeInfo.MessageCountDetails.ActiveMessageCount}\n " +
                $"Queue Creation time: {runtimeInfo.CreatedAt}\n" +
                $"Queue last updation time: {runtimeInfo.UpdatedAt}\n");
        }
    }
}