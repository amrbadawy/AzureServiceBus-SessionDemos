using Extensions;
using Microsoft.Azure.ServiceBus;

namespace ChatGroupApp
{
    class ChatMessageManager : IChatMessageManager
    {
        private readonly System.Windows.Forms.ListBox _chatArea;
        public ChatMessageManager(System.Windows.Forms.ListBox chatArea)
        {
            _chatArea = chatArea;
        }

        public void Show(Message message)
        {
            var time = message.SystemProperties.EnqueuedTimeUtc.ToLocalTime().ToString("hh:mm:ss");
            var username = message.Label;
            var sequenceNumber = message.SystemProperties.SequenceNumber;
            var text = message.Body.AsString();

            text = Encryption.TryDecrypt(text, username);

            Show($"{time} - {username} » {text} #({sequenceNumber})");
        }

        public void Show(string message)
        {
            _chatArea.Invoke(ctrl => ctrl.Items.Add(message));
            _chatArea.Invoke(ctrl => ctrl.SelectedIndex = ctrl.Items.Count - 1);
        }
    }
}
