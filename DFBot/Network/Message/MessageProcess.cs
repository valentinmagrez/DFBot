using DFBot.States;
using System.Linq;

namespace DFBot.Network.Message
{
    public class MessageProcess
    {
        public Bot Bot { get; set; }
        
        public MessageProcess(Bot bot)
        {
            Bot = bot;
        }
        
        /// <summary>
        /// Compute the message received by the socket
        /// </summary>
        /// <param name="messageReceived"></param>
        public int ProcessReceivedMessage(string messageReceived)
        {
            var separatedData = DecomposeMessage(messageReceived);

            foreach (var data in separatedData)
            {
                ProcessEachMessageIndividually(data);
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
        /// <param name="messageReceived">Message received by the socket</param>
        private void ProcessEachMessageIndividually(string messageReceived)
        {
            var s = Bot.State;

            foreach (var m in s.MessagesKnown)
            {
                if (!messageReceived.StartsWith(m.Value))
                    continue;

                s.Command.Execute(messageReceived);

                return;
            }
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
