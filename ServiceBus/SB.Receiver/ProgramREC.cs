using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;
using Extensions;

namespace SB.Receiver
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Print.White("Press any Key to RECEIVE ...");
                    Console.ReadKey();

                    Print.Yellow("Start Receiving from Queue ...");
                    await Start();
                    Print.Green("Done √√");
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
            await ReceiveFromQueue();
            
            //ReceiveFromSubs();

            //await ReceiveSessionMessages(Session_Queue_01);
            //await ReceiveSessionMessages(Session_Queue_01);
            //await ReceiveSessionMessages(Session_Queue_01);

            await Task.CompletedTask;
        }
    }
}
