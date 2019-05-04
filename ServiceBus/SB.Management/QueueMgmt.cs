using Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Management
{
    partial class Program
    {
        private static async Task ManageQueue()
        {
            var queueName = AzureRiyadh_Queue_02;// Guid.NewGuid().ToString("D").Substring(0, 8);

            await CreateQueueIfNotExists(queueName);
            //await UpdateQueue(queueDesc);
            await GetQueueRuntimeInfo(queueName);
        }
        private static async Task CreateSessionQueue()
        {
            var queueDescription = CreateQueueDescription(Session_Queue_01);
            queueDescription.RequiresSession = true;
            queueDescription.RequiresDuplicateDetection = false;

            await CreateQueueIfNotExists(queueDescription);
        }

        #region • Main Func •

        private static async Task<QueueDescription> CreateQueueIfNotExists(string queueName)
        {
            try
            {
                //await _mgmtClient.QueueExistsAsync(queueName);
                return await _mgmtClient.GetQueueAsync(queueName);
            }
            catch (MessagingEntityNotFoundException)
            {
                var queueDescription = CreateQueueDescription(queueName);
                return await _mgmtClient.CreateQueueAsync(queueDescription);
            }
        }
        private static async Task<QueueDescription> CreateQueueIfNotExists(QueueDescription queueDescription)
        {
            try
            {
                return await _mgmtClient.GetQueueAsync(queueDescription.Path);
            }
            catch (MessagingEntityNotFoundException)
            {
                return await _mgmtClient.CreateQueueAsync(queueDescription);
            }
        }

        private static QueueDescription CreateQueueDescription(string queueName)
        {
            return new QueueDescription(queueName)
            {
                // The maximum delivery count of a message before it is dead-lettered
                // default value = 10
                // Min value = 1
                // Max value = int.Max
                MaxDeliveryCount = 5,

                // default value = 60 seconds.
                // Max value = 5 minutes.
                LockDuration = TimeSpan.FromSeconds(45),

                // The broker doesn't allow changing of this property at run-time.
                // You will have to delete your entity and re-create it.
                RequiresDuplicateDetection = true,

                // Message Retention Period
                // default value = 1 minute.
                // Max value = 7 days.
                // Min value = 20 seconds.
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(2),

                // Must be one of the following values: 1024; 2048; 3072; 4096; 5120
                MaxSizeInMB = 1024,

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
                EnableBatchedOperations = false,

                // Creating only one partition. 
                EnablePartitioning = false
            };
        }

        private static async Task UpdateQueue(string queuePath)
        {
            var queue = await _mgmtClient.GetQueueAsync(queuePath);
            await UpdateQueue(queue);
        }
        private static async Task UpdateQueue(QueueDescription queueDesc)
        {
            queueDesc.MaxDeliveryCount = 10;
            queueDesc.LockDuration = TimeSpan.FromSeconds(5);
            //queueDesc.RequiresDuplicateDetection = true;

            await _mgmtClient.UpdateQueueAsync(queueDesc);
        }
        
        private static async Task GetQueueRuntimeInfo(string queueName)
        {
            var queueRuntimeInfo = await _mgmtClient.GetQueueRuntimeInfoAsync(queueName).ConfigureAwait(false);
            Console.WriteLine($"Retrieved runtime information of queue\n " +
                $"Active messages:{queueRuntimeInfo.MessageCountDetails.ActiveMessageCount}\n " +
                $"Messages:{queueRuntimeInfo.MessageCountDetails.Serialize()}\n " +
                $"Size of queue:{queueRuntimeInfo.SizeInBytes}\n" +
                $"Queue Creation time: {queueRuntimeInfo.CreatedAt}\n" +
                $"Queue last updation time: {queueRuntimeInfo.UpdatedAt}\n");
        } 

        #endregion
    }
}