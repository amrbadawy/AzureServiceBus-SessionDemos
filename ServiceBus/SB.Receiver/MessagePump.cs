using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Text;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;
using Microsoft.Azure.ServiceBus;
using Extensions;
using System.Threading;
using System.Drawing;

namespace SB.Receiver
{
    partial class Program
    {
        static MessageReceiver _pumpReceiver;
        static void RegisterMessageHandler()
        {
            _pumpReceiver = new MessageReceiver(SBConnectionString, AzureRiyadh_Queue_01, ReceiveMode.ReceiveAndDelete);
            RegisterMessageHandler(_pumpReceiver);
        }

        static void RegisterMessageHandler(MessageReceiver messageReceiver)
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
            var sequenceNumber = message.SystemProperties.SequenceNumber;
            var body = Encoding.UTF8.GetString(message.Body);

            var messageToShow = $"{sequenceNumber} :: {body}";

            Print.WithColor(messageToShow, Color.AliceBlue);
            //await pumpReceiver.CompleteAsync(message.SystemProperties.LockToken);
            await Task.CompletedTask;
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception {args.Exception}.");

            var context = args.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
