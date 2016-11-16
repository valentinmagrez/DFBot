using DFBot.Network;
using DFBot.Network.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DFBot
{
    public class CommunicationThread
    {
        private SocketDof _socket;

        public CommunicationThread(SocketDof socket)
        {
            _socket = socket;
        }

        public void launch()
        {
            _socket.SocketConnection();
            while (true)
            {

            }
        }
    }
}
