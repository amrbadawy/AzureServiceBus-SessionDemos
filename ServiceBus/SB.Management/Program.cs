using Extensions;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Management
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Print.White("Press any Key to Start ...");
                    Console.ReadKey();

                    Print.Yellow("Start Managing Entites ...");

                    await Test();

                    Print.Done("Done √√");
                }
            }
            catch (Exception exception)
            {
                Print.Exception(exception);
            }
            Console.ReadLine();
        }
    }
}