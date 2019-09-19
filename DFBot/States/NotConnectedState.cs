using DFBot.Enum;
using DFBot.Network.Message;
using System.Collections.Generic;
using DFBot.Action.ConnectChar;

namespace DFBot.States
{
    public class NotConnectedState : State
    {
        public NotConnectedState(Bot bot)
        {
            Value = StateType.NotConnected;
            Command = new ConnectionCommand(bot);
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.Connection.ReceivedKey),
                new MessageType(PrefixMessage.Connection.DisconnectSomeone),
                new MessageType(PrefixMessage.Connection.ConnectionOk)
            };
        }
    }
}
