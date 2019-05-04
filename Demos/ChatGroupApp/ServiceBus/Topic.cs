using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Threading.Tasks;
using static ChatGroupApp.WorkshopConfig;

namespace ChatGroupApp
{
    internal static class Topic
    {
        public static ManagementClient MgmtClient { get; } = new ManagementClient(SBConnectionString);

        public static async Task<TopicDescription> CreateTopicIfNotExists(string name)
        {
            try
            {
                return await MgmtClient.GetTopicAsync(name);
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
                RequiresDuplicateDetection = false,

                // Message Retention Period
                // default value = 1 minute.
                // Min value = 20 seconds.
                // Max value = 7 days.
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

            return await MgmtClient.CreateTopicAsync(description);
        }

        public static async Task Enable(string topicName)
        {
            var topic = await MgmtClient.GetTopicAsync(topicName);
            topic.Status = EntityStatus.Active;

            await MgmtClient.UpdateTopicAsync(topic);
        }
        public static async Task Disable(string topicName)
        {
            var topic = await MgmtClient.GetTopicAsync(topicName);
            topic.Status = EntityStatus.Disabled;

            await MgmtClient.UpdateTopicAsync(topic);
        }
        public static async Task UpdateTopic(TopicDescription topic)
        {
            topic.DefaultMessageTimeToLive = TimeSpan.FromDays(6);
            topic.Status = EntityStatus.SendDisabled;

            await MgmtClient.UpdateTopicAsync(topic);
        }
    }
}