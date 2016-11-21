using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Action.InGame
{
    public class MapCommand : Command
    {
        private Bot _bot;

        public MapCommand(Bot bot = null)
        {
            _bot = bot;
        }

        public int Execute(string message)
        {
            return _bot.InGame(message);
        }

        public void AttachBot(Bot bot)
        {
            _bot = bot;
        }
    }
}
