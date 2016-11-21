using DFBot.Action;
using DFBot.Action.ConnectPerso;
using DFBot.Enum;
using DFBot.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DFBot.Network.Message
{
    public class MessageProcess
    {

        public Bot _bot;
        
        public MessageProcess(Bot bot)
        {
            _bot = bot;
        }
        
        /// <summary>
        /// Compute the message received by the socket
        /// </summary>
        /// <param name="messageReceived"></param>
        public int ProcessReceivedMessage(string messageReceived)
        {
            string[] separatedData = DecomposeMessage(messageReceived);

            foreach (string data in separatedData)
            {
                var i=ProcessEachMessageIndividually(data);
            }

            return 0;
        }

        /// <summary>
        /// Parse the whole message received by the socket
        /// into an array of simple message
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <returns></returns>
        private string[] DecomposeMessage(string receivedMessage)
        {
            return receivedMessage.Split((char)0)
                .Where(x => x.Length > 0).ToArray();
        }

        /// <summary>
        /// Analyse one message and decompose it into 
        /// a Message object
        /// </summary>
        /// <param name="message">Message received by the socket</param>
        private int ProcessEachMessageIndividually(string messageReceived)
        {
            State s = _bot.State;

            foreach (MessageType m in s.MessagesKnown)
            {
                if (messageReceived.StartsWith(m.Value))
                {
                    s.Command.AttachBot(_bot);
                    return s.Command.Execute(messageReceived);
                }
            }
            return 0;
        }

        public string BuildMessage(State state, string content)
        {
            switch (state.Value)
            {
                default:
                    return "";
            }
        }
    }
}
