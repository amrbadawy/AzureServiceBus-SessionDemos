using Microsoft.Azure.ServiceBus;
using System;

namespace ChatGroupApp
{
    internal interface IChatMessageManager
    {
        void Show(Message message);
        void Show(string message);
    }
}
