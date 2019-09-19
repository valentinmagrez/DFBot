using DFBot.Action.InGame;
using DFBot.Enum;
using DFBot.Network.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.States
{
    public class InGameState : State
    {
        public InGameState(Bot bot)
        {
            Value = StateType.InGame;
            Command = new MapCommand(bot);
            MessagesKnown = new List<MessageType>{
                new MessageType(PrefixMessage.MapLoading.GetInfo)
            };
        }
    }
}
