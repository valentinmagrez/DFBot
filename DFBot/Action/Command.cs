using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Action
{
    /// <summary>
    /// Using the command pattern
    /// Command interface
    /// </summary>
    public interface Command
    {
        /// <summary>
        /// Execute the specific action
        /// associated to the command
        /// </summary>
        /// <param name="message">message to communicate to the client</param>
        int Execute(string message);

        /// <summary>
        /// Associate the bot object to the command
        /// </summary>
        /// <param name="bot"></param>
        void AttachBot(Bot bot);
    }
}
