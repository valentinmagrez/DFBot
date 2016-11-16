using DFBot.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot
{
    public class Engine
    {
        public List<Command> commands;

        public void execute(Command cmd)
        {
            cmd.Execute("test");
        }
    }
}
