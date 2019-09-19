using DFBot.Enum;
using DFBot.Network.Message;
using System.Collections.Generic;
using DFBot.Action.ConnectChar;

namespace DFBot.States
{
    public class CharacterSelectionState : State
    {
        public CharacterSelectionState(Bot bot)
        {
            Value = StateType.ServerSelected;
            Command = new SelectCharacterCommand(bot);
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.CharacterSelection.PersoInfo),
                new MessageType(PrefixMessage.CharacterSelection.PersoConnected),
                new MessageType(PrefixMessage.CharacterSelection.MapDetails)
            };
        }
    }
}
