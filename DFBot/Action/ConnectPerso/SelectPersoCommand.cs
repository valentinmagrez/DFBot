using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Action.ConnectPerso
{
    /// <summary>
    /// Manage the selection of the perso
    /// </summary>
    public class SelectPersoCommand : Command
    {
        private Bot _bot;

        public SelectPersoCommand(Bot bot=null)
        {
            _bot = bot;
        }

        public int Execute(string message)
        {
            return _bot.PersoSelection(message);
        }

        public void AttachBot(Bot bot)
        {
            _bot = bot;
        }
    }
}
