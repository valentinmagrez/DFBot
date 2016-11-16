using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Enum
{
    public class PrefixMessage
    {
        public class Connection
        {
            #region connection
            public const string ReceivedKey = "HC";
            public const string DisconnectSomeone = "Aled";
            public const string ConnectionOk = "Ad";
            #endregion
        }

        public class ServerSelection{
            #region select server
            public const string ServersInfo = "AH";
            public const string SelectServer = "AX";
            public const string SwitchSocketOK = "HG";
            public const string ServerIdentification = "AT";
            public const string PlaceQueue = "Aq";
            #endregion
        }

        public class PersoSelection
        {
            #region select perso
            public const string PersoInfo = "ALK";
            #endregion
        }
    }
}
