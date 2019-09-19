namespace DFBot.Enum
{
    public class PrefixMessage
    {
        public class Connection
        {
            public const string ReceivedKey = "HC";
            public const string DisconnectSomeone = "Aled";
            public const string ConnectionOk = "Ad";
        }

        public class ServerSelection{
            public const string ServersInfo = "AH";
            public const string SelectServer = "AX";
            public const string SwitchSocketOk = "HG";
            public const string ServerIdentification = "AT";
            public const string PlaceQueue = "Aq";
        }

        public class CharacterSelection
        {
            public const string PersoInfo = "ALK";
            public const string PersoConnected = "ASK";
            public const string MapDetails = "GCK";
        }

        public class MapLoading
        {
            public const string AskInfo = "GI";
            public const string GetInfo = "GM";
        }
    }
}
