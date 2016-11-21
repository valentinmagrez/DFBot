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
    public class PersoSelectionState : State
    {
        public PersoSelectionState()
        {
            Value = StateType.ServerSelected;
            Command = new SelectPersoCommand();
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.PersoSelection.PersoInfo),
                new MessageType(PrefixMessage.PersoSelection.PersoConnected),
                new MessageType(PrefixMessage.PersoSelection.MapDetails)
            };
        }
    }
}
