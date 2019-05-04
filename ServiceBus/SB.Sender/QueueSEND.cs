using Extensions;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Threading.Tasks;
using static SB.Utils.SampleMessages;
using static SB.Utils.WorkshopConfig;
using Microsoft.Azure.ServiceBus;
using System.IO;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus.Plugins;

namespace SB.Sender
{
    partial class Program
    {
        private static MessageSender q1Sender;
        private static async Task SendToQueue()
        {
            q1Sender = new MessageSender(SBConnectionString, AzureRiyadh_Queue_01);
            //q1Sender.RegisterPlugin(new MeetupSendPlugin());
            //q1Sender.RegisterPlugin(new FixedMessageIdSendPlugin("1122"));


            // 1. Text
            var txtMsg = $"Azure Riyadh Meetup :: Test Message #{DateTime.Now.Ticks}"; //txtMsg = new StringBuilder().Append('M', 1500000).ToString();
            await Send(q1Sender, txtMsg);
            await Schedule(q1Sender, txtMsg);

            // 2. Serialize Object
            dynamic address = new { Country = "SA", City = "Riyadh", Street = "Olaya St" };
            await Send(q1Sender, Json.SerializeD(address));

            // 3. Image
            //await SendBlob(q1Sender, Blob01);

            // 4. Object
            await SendAttendee(q1Sender);

            await q1Sender.CloseAsync();
        }

        private static async Task Send(MessageSender sender, string data)
        {
            var message = new Message
            {
                //MessageId = 
                Body = data.ToBytes(),
                TimeToLive = TimeSpan.FromSeconds(1000)
            };
            await sender.SendAsync(message);
        }
        private static async Task Schedule(MessageSender sender, string data)
        {
            var message = new Message
            {
                //MessageId = 
                Body = data.ToBytes(),
                TimeToLive = TimeSpan.FromSeconds(1000)
            };
            await sender.ScheduleMessageAsync(message, DateTimeOffset.Now.AddSeconds(40));
        }
        private static async Task SendAttendee(MessageSender messageSender, string sessionId = null)
        {
            var tasks = new List<Task>();
            foreach (var attendee in Attendees)
            {
                // JsonConvert.SerializeObject(data);
                // Encoding.UTF8.GetBytes(data)
                var messageBody = attendee.Serialize().ToBytes();

                var message = new Message
                {
                    Body = messageBody,
                    TimeToLive = TimeSpan.FromMinutes(10),
                    //MessageId = attendee.Id.ToString(),

                    UserProperties =
                    {
                        { "City", attendee.City },
                        { "Category", attendee.Category }
                    }
                };

                if (sessionId.IsNotNullOrWhiteSpace())
                    message.SessionId = sessionId;

                var task = messageSender.SendAsync(message);

                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }
        private static async Task SendBlob(MessageSender messageSender, string fileName)
        {
            var messageBody = File.ReadAllBytes(BlobsPath + fileName);

            var message = new Message
            {
                Body = messageBody,
                TimeToLive = TimeSpan.FromSeconds(1000)
            };

            await messageSender.SendAsync(message);
        }

        private static async Task SendToSessionQueue()
        {
            q1Sender = new MessageSender(SBConnectionString, Session_Queue_01);

            var t1 = SendAttendee(q1Sender, "100");
            var t2 = SendAttendee(q1Sender, "200");
            var t3 = SendAttendee(q1Sender, "300");
            var t4 = SendAttendee(q1Sender, "400");

            await Task.WhenAll(t1, t2, t3, t4);
            await q1Sender.CloseAsync();
        }
    }
}
