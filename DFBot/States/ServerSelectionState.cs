using DFBot.Enum;
using DFBot.Network.Message;
using System.Collections.Generic;
using DFBot.Action.ConnectChar;

namespace DFBot.States
{
    public class ServerSelectionState : State
    {
        public ServerSelectionState(Bot bot)
        {
            Value = StateType.Connected;
            Command = new SelectServerCommand(bot);
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.ServerSelection.ServersInfo),
                new MessageType(PrefixMessage.ServerSelection.SelectServer),
                new MessageType(PrefixMessage.ServerSelection.SwitchSocketOk),
                new MessageType(PrefixMessage.ServerSelection.ServerIdentification),
                new MessageType(PrefixMessage.ServerSelection.PlaceQueue),
                new MessageType(PrefixMessage.CharacterSelection.PersoInfo)
            };
        }
    }
}
