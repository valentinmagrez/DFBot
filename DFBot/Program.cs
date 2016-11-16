using DFBot.Action.ConnectPerso;
using DFBot.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DFBot
{
    public class Program
    {

        public static readonly log4net.ILog log =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Account account = new Account("account", "pwd");
            Perso perso = new Perso("pseudo");

            Bot bot = new Bot(account, perso);

            SocketDof s = new SocketDof("80.239.173.166", 443, bot);
            log.Info("Application is working");


            //SocketDof s = new SocketDof("80.239.173.166", 443);
            CommunicationThread t = new CommunicationThread(s);

            t.launch();
        }
    }
}
