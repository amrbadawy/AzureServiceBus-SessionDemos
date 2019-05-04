using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using SB.Utils;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Management
{
    partial class Program
    {
        private static async Task ManageOrderTopic()
        {
            var desc = await CreateTopicIfNotExists(OrderTopic.TopicName);
            var desc1 = await CreateSubscriptionIfNotExists(OrderTopic.TopicName, OrderTopic.Sub01);
            var desc2 = await CreateSubscriptionIfNotExists(OrderTopic.TopicName, OrderTopic.Sub02);
            var desc3 = await CreateSubscriptionIfNotExists(OrderTopic.TopicName, OrderTopic.Sub03);

            //await UpdateTopic(desc);
            //await GeTopicRuntimeInfo(OrderTopic.TopicName);
        }
        private static async Task ManageAttendeeTopic()
        {
            var desc = await CreateTopicIfNotExists(AttendeeTopic.TopicName);

            await CreateSubscriptionIfNotExists(AttendeeTopic.TopicName, AttendeeTopic.All);

            var ruhRule = new RuleDescription
            {
                Name = "Riyadh_SqlFilter",
                Filter = new SqlFilter($"City = '{Cities.Riyadh}'") // OR City = '{Cities.Riyadh}'")
                //Action = new SqlRuleAction("SET XYZ = 'Value'")
            };
            await CreateSubscriptionIfNotExists(AttendeeTopic.TopicName, AttendeeTopic.Riaydh, ruhRule);

            var vipRule = new RuleDescription
            {
                Name = "Vip_SqlFilter",
                Filter = new SqlFilter($"Category = '{Categories.VIP}'")
            };
            await CreateSubscriptionIfNotExists(AttendeeTopic.TopicName, AttendeeTopic.VIP, vipRule);
        }
        private static async Task<TopicDescription> CreateTopicIfNotExists(string name)
        {
            try
            {
                return await _mgmtClient.GetTopicAsync(name);
            }
            catch (MessagingEntityNotFoundException)
            {
                return await CreateTopic(name);
            }
        }
        private static async Task<TopicDescription> CreateTopic(string name)
        {
            var description = new TopicDescription(name)
            {
                RequiresDuplicateDetection = true,
                // Message Retention Period
                // default value = 1 minute.
                // Max value = 7 days.
                // Min value = 20 seconds.
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(2),

                MaxSizeInMB = 1024,

                // >> Message >>
                // ExpiresAtUtc = EnqueuedTimeUtc + TimeToLive
                DefaultMessageTimeToLive = TimeSpan.FromDays(7), // Expiration

                // Duration of idle interval after which the queue is automatically deleted. 
                // Default / Max value = TimeSpan.MaxValue >> 29K years
                // Min value = 5 minute
                AutoDeleteOnIdle = TimeSpan.MaxValue,

                EnableBatchedOperations = false,

                // Creating only one partition. 
                EnablePartitioning = false
            };

            return await _mgmtClient.CreateTopicAsync(description);
        }
        private static async Task UpdateTopic(TopicDescription description)
        {
            description.DefaultMessageTimeToLive = TimeSpan.FromDays(6);
            description.RequiresDuplicateDetection = false;

            await _mgmtClient.UpdateTopicAsync(description);
        }
        private static async Task GeTopicRuntimeInfo(string name)
        {
            var runtimeInfo = await _mgmtClient.GetTopicRuntimeInfoAsync(name).ConfigureAwait(false);
            Console.WriteLine($"Retrieved runtime information \n " +
                $"Active messages:{runtimeInfo.MessageCountDetails.ActiveMessageCount}\n " +
                $"Size :{runtimeInfo.SizeInBytes}\n" +
                $"Queue Creation time: {runtimeInfo.CreatedAt}\n" +
                $"Queue last updation time: {runtimeInfo.UpdatedAt}\n");
        }
    }
}