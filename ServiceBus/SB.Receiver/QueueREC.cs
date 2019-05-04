using Microsoft.Azure.ServiceBus.Core;
using SB.Utils;
using System;
using System.Text;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;
using Microsoft.Azure.ServiceBus;
using System.IO;
using Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SB.Receiver
{
    partial class Program
    {
        static async Task ReceiveFromQueue()
        {
            //await ReceiveAttendee();
            //await ReceiveBlob();
            //await Peek();
            //RegisterMessageHandler();

            for (int i = 0; i < SampleMessages.Attendees.Count(); i++)
            {
                await ReceiveAttendeePeek();
            }

            await Task.CompletedTask;
        }

        private static async Task ReceiveAttendee()
        {
            var messageBody = await ReceiveAndDelete();
            if (messageBody != null)
            {
                var bodyAsString = Encoding.UTF8.GetString(messageBody);
                var attendee = bodyAsString.Deserialize<MeetupAttendee>();

                Print.White(attendee.Serialize());
            }
        }

        private static async Task ReceiveBlob()
        {
            var messageBody = await ReceiveAndDelete();
            if (messageBody != null)
            {
                var filePath = $@"C:\AzureMessageFile\AzureImage{DateTime.Now.Ticks}.png";

                File.WriteAllBytes(filePath, messageBody);
            }
        }

        private static async Task<byte[]> ReceiveAndDelete()
        {
            var receiver = new MessageReceiver(SBConnectionString, AzureRiyadh_Queue_01, ReceiveMode.ReceiveAndDelete);
            var message = await receiver.ReceiveAsync(TimeSpan.FromSeconds(1));

            return message?.Body;
        }

        private static async Task ReceiveAttendeePeek()
        {
            var receiver = new MessageReceiver(SBConnectionString, AzureRiyadh_Queue_01, ReceiveMode.PeekLock);
            var message = await receiver.ReceiveAsync(TimeSpan.FromSeconds(1));

            try
            {
                if (message.Body != null)
                {
                    var bodyAsString = Encoding.UTF8.GetString(message.Body);
                    var attendee = bodyAsString.Deserialize<MeetupAttendee>();

                    //throw new Exception($"Exception By DEV @ {DateTime.Now}");
                    //throw new ArgumentException($"ArgumentException By DEV @ {DateTime.Now}");

                    Print.White(attendee.Serialize());
                }
                await receiver.CompleteAsync(message.SystemProperties.LockToken);
            }
            catch (ArgumentException exception)
            {
                await receiver.DeadLetterAsync(message.SystemProperties.LockToken,
                    "ArgumentException" , exception.Message);
            }
            catch (Exception exception)
            {
                var propertiesToModify = new Dictionary<string, object>()
                {
                    { "AbandonReason", exception.Message }
                };
                await receiver.AbandonAsync(message.SystemProperties.LockToken, propertiesToModify);
            }
        }

        private static async Task Peek()
        {
            var receiver = new MessageReceiver(SBConnectionString, AzureRiyadh_Queue_01, ReceiveMode.ReceiveAndDelete);

            var messages = await receiver.PeekAsync(5);
            foreach (var msg in messages.EmptyIfNull())
            {
                var bodyAsString = Encoding.UTF8.GetString(msg.Body);
                var seqNo = msg.SystemProperties.SequenceNumber;
                Print.White($"{seqNo}-{bodyAsString}");
            }
        }

        private static async Task ReceiveSessionMessages(string queueName)
        {
            var sessionClient = new SessionClient(SBConnectionString, queueName, ReceiveMode.ReceiveAndDelete);

            var session = await sessionClient.AcceptMessageSessionAsync();
            if (session != null)
            {
                while (true)
                {
                    var msg = await session.ReceiveAsync(TimeSpan.FromSeconds(2));
                    if (msg == null)
                        break;

                    var bodyAsString = Encoding.UTF8.GetString(msg.Body);
                    var seqNo = msg.SystemProperties.SequenceNumber;
                    Print.White($"{seqNo}--{msg.SessionId}--{bodyAsString}");
                }

                Console.WriteLine($"Received all messages for Session: {session.SessionId}");
                Console.WriteLine("=====================================");
                await session.CloseAsync();
            }
        }
    }
}
