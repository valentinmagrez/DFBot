using DFBot.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Network.Message
{
    public sealed class MessageType
    {
        /// <summary>
        /// Value of the enum type
        /// </summary>
        private readonly string _value;
        public string Value
        {
            get { return _value; }
        }

        public MessageType(string value)
        {
            _value = value;
        }

        override
        public string ToString()
        {
            return _value;
        }
    }
}
