using DFBot.Action;
using DFBot.Network.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private StateType _value;
        public StateType Value
        {
            get { return _value; }
            protected set { _value = value; }
        }

        private List<MessageType> _messagesKnown = new List<MessageType>();
        public List<MessageType> MessagesKnown 
        {
            get { return _messagesKnown; }
            protected set { _messagesKnown = value; }
        }
        
        private Command _command;
        public Command Command
        {
            get { return _command; }
            protected set { _command = value; }
        }


    }
}
