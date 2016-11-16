using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Action.ConnectPerso
{
    /// <summary>
    /// MAnage the connection of the account
    /// </summary>
    class ConnectionCommand : Command
    {
        private Bot _bot;

        public ConnectionCommand(Bot bot=null)
        {
            _bot = bot;
        }

        public int Execute(string message)
        {
            return _bot.Connection(message);
        }

        public void AttachBot(Bot bot)
        {
            _bot = bot;
        }
    }
}
