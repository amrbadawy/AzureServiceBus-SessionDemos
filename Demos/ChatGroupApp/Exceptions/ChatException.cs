using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGroupApp
{
    internal class ChatException : Exception
    {
        public ChatException(string message) : base(message) { }
        public ChatException(string message, Exception inner) : base(message, inner) { }
    }
}
