using Extensions;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Threading.Tasks;
using static SB.Utils.WorkshopConfig;

namespace SB.Management
{
    partial class Program
    {
        private static ManagementClient _mgmtClient = new ManagementClient(SBConnectionString);

        static async Task Test()
        {
            await ManageQueue();
            //await UpdateQueue(AzureRiyadh_Queue_01);

            //await ManageOrderTopic();

            //await ManageAttendeeTopic();

            //await CreateSessionQueue();
        }
    }
}