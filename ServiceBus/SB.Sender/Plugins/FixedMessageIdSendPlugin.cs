using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Extensions;

namespace SB.Sender
{
    public class FixedMessageIdSendPlugin : ServiceBusPlugin
    {
        public string MessageId { get; }
        public FixedMessageIdSendPlugin(string messageId)
        {
            Guard.IsEmpty(messageId);
            MessageId = messageId;
        }
        public override string Name => nameof(FixedMessageIdSendPlugin);
        public override Task<Message> BeforeMessageSend(Message message)
        {
            message.MessageId = MessageId;
            return Task.FromResult(message);
        }
    }
}
