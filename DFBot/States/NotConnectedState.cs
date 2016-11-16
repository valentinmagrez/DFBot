using DFBot.Action.ConnectPerso;
using DFBot.Enum;
using DFBot.Network.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.States
{
    public class NotConnectedState : State
    {
        public NotConnectedState()
        {
            Value = StateType.NotConnected;
            Command = new ConnectionCommand();
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.Connection.ReceivedKey),
                new MessageType(PrefixMessage.Connection.DisconnectSomeone),
                new MessageType(PrefixMessage.Connection.ConnectionOk)
            };
        }
    }
}
