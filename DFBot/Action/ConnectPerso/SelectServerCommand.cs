using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Action.ConnectPerso
{
    /// <summary>
    /// Manage the selection of the server
    /// </summary>
    public class SelectServerCommand : Command
    {
        private Bot _bot;

        public SelectServerCommand(Bot bot=null)
        {
            _bot = bot;
        }

        public int Execute(string message)
        {
           return _bot.ServerSelection(message);
        }

        public void AttachBot(Bot bot)
        {
            _bot = bot;
        }
    }
}
