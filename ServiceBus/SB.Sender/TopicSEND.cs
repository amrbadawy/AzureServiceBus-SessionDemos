using Extensions;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Sender
{
    partial class Program
    {
        private static async Task SendToOrderTopic()
        {
            var orderTopicSender = new MessageSender(SBConnectionString, OrderTopic.TopicName);

            // 1. Text
            var txtMsg = $"Azure Riyadh Meetup :: Test Message #{DateTime.Now.Ticks}";
            //txtMsg = new StringBuilder().Append('M', 1500000).ToString();
            await Send(orderTopicSender, txtMsg);

            // 2. Serialize Object
            dynamic address = new { Country = "SA", City = "Riyadh", Street = "Olaya St" };
            await Send(orderTopicSender, Json.SerializeD(address));

            // 3. Image
            //await SendBlob(q1Sender, Blob01);

            // 4. Object
            await SendAttendee(orderTopicSender);

            await orderTopicSender.CloseAsync();
        }

        private static async Task SendTo_Attendee_Topic()
        {
            var orderTopicSender = new MessageSender(SBConnectionString, AttendeeTopic.TopicName);

            await SendAttendee(orderTopicSender);

            await orderTopicSender.CloseAsync();
        }
    }
}
