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
    public class ServerSelectionState : State
    {
        public ServerSelectionState()
        {
            Value = StateType.Connected;
            Command = new SelectServerCommand();
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.ServerSelection.ServersInfo),
                new MessageType(PrefixMessage.ServerSelection.SelectServer),
                new MessageType(PrefixMessage.ServerSelection.SwitchSocketOK),
                new MessageType(PrefixMessage.ServerSelection.ServerIdentification),
                new MessageType("BN"),
                new MessageType("AV0"),
                new MessageType(PrefixMessage.ServerSelection.PlaceQueue)
            };
        }
    }
}
