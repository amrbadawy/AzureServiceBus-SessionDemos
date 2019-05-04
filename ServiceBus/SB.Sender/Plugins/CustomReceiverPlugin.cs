using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace SB.Sender
{
    public class CustomReceiverPlugin : ServiceBusPlugin
    {
        public override string Name => nameof(CustomReceiverPlugin);
        public override Task<Message> AfterMessageReceive(Message message)
        {
            return Task.FromResult(message);
        }
    }
}
//var messageIdPlugin = new MessageIdPlugin((msg) => Guid.NewGuid().ToString("N"));
//sender.RegisterPlugin(messageIdPlugin);
