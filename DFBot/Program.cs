using DFBot.Action.ConnectPerso;
using DFBot.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace DFBot
{
    public class Program
    {

        private static string _account;
        private static string _pwd;
        private static string _pseudo;

        public static readonly log4net.ILog log =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static void LoadConfig()
        {

            XElement bot = XElement.Load("info.xml").Element("bot");

            _account = bot.Element("account").Element("identifiant").Value;
            _pwd = bot.Element("account").Element("password").Value;
            _pseudo = bot.Element("perso").Element("pseudo").Value;
        }

        static void Main(string[] args)
        {
            LoadConfig();
            Account account = new Account(_account, _pwd);
            Perso perso = new Perso(_pseudo);

            Bot bot = new Bot(account, perso);

            SocketDof s = new SocketDof("80.239.173.166", 443, bot);
            log.Info("Application is working");
            CommunicationThread t = new CommunicationThread(s);

            t.launch();
        }
    }
}
