using Extensions;
using System;
using System.Threading.Tasks;

namespace SB.Sender
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Print.White("Press any Key to SEND ...");
                    Console.ReadKey();

                    Print.Yellow("Start Sending ...");

                    await Start();

                    Print.Done("Done √√");
                }
            }
            catch (Exception exception)
            {
                Print.Exception(exception);
            }
            Console.ReadLine();
        }

        static async Task Start()
        {
            await SendToQueue();
            
            //await SendToOrderTopic();
            
            //await SendTo_Attendee_Topic();
            
            //await SendToSessionQueue();
        }
    }
}
