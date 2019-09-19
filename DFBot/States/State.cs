using DFBot.Action;
using DFBot.Network.Message;
using System.Collections.Generic;

namespace DFBot.States
{
    public enum StateType
    {
        NotConnected,
        Connected,
        ServerSelected,
        InGame,
        InFight
    }

    public class State
    {
        public StateType Value { get; protected set; }

        public List<MessageType> MessagesKnown { get; protected set; } = new List<MessageType>();

        public ICommand Command { get; protected set; }
    }
}
