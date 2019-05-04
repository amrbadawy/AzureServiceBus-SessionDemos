using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;
using Microsoft.Azure.ServiceBus;
using System;

namespace SB.Receiver
{
    partial class Program
    {
        static void ReceiveFromSubs()
        {
            Console.WriteLine("Enter Subscription Name:");
            var subname = Console.ReadLine();

            ReceiveFromSub(AttendeeTopic.TopicName, subname);
        }

        static void ReceiveFromSub(string topic, string sub)
        {
            var entityPath = EntityNameHelper.FormatSubscriptionPath(topic, sub);
            var receiver = new MessageReceiver(SBConnectionString, entityPath, ReceiveMode.ReceiveAndDelete);

            RegisterMessageHandler(receiver);
        }
    }
}
