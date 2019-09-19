using DFBot.Network;
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

            var bot = XElement.Load("info.xml").Element("bot");

            _account = bot.Element("account").Element("identifiant").Value;
            _pwd = bot.Element("account").Element("password").Value;
            _pseudo = bot.Element("perso").Element("pseudo").Value;
        }

        static void Main(string[] args)
        {
            LoadConfig();
            var account = new Account(_account, _pwd);
            var perso = new Character(_pseudo);

            var bot = new Bot(account, perso);
            bot.Init();

            var s = new SocketDof("34.251.172.139", 443, bot);
            log.Info("Application is working");
            var t = new CommunicationThread(s);

            t.launch();
        }
    }
}
