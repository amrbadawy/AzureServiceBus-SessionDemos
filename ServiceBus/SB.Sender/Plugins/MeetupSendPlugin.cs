using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace SB.Sender
{
    public class MeetupSendPlugin : ServiceBusPlugin
    {
        public override string Name => nameof(MeetupSendPlugin);
        public override Task<Message> BeforeMessageSend(Message message)
        {
            message.UserProperties.Add("Code", "AzureMeetup");
            return Task.FromResult(message);
        }
    }
}
