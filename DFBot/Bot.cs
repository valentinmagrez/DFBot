using System;
using System.Collections.Generic;
using DFBot.Enum;
using DFBot.Network;
using DFBot.States;
using System.Threading;
using DFBot.Lib;
using DFBot.Map;

namespace DFBot
{
    public class Bot
    {
        private const char CharNewLine= (char)10;
        private const char CharNull = (char)0;

        private const int IDSERVER = 601;
        private IDictionary<String, String> Cache = new Dictionary<String, String>();
        private MapLoader _mapLoader = new MapLoader();

        public Bot(Account account, Character character)
        {
            Account = account;
            Character = character;
        }
        public Account Account { get; }

        public Character Character { get; }

        public State State { get; set; }

        public SocketDof Socket { get; set; }

        public void Init()
        {
            State = new NotConnectedState(this);
        }

        public int Connection(string message)
        {
            if (message.StartsWith(PrefixMessage.Connection.ReceivedKey))
            {
                Account.Key = message.Remove(0,PrefixMessage.Connection.ReceivedKey.Length);

                ConnectCharacter();
            }
            else if (message.StartsWith(PrefixMessage.Connection.DisconnectSomeone))
            {
                ConnectCharacter();
            }
            else if (message.StartsWith(PrefixMessage.Connection.ConnectionOk))
            {
                State = new ServerSelectionState(this);
            }
            return 0;
        }

        public int ServerSelection(string message)
        {
            if (message.StartsWith(PrefixMessage.ServerSelection.ServersInfo))
            {
                SelectServer();
            }
            else if (message.StartsWith(PrefixMessage.ServerSelection.SelectServer))
            {
                var ip = SeverIPDecrypt.Decrypt(message);
                //Switch socket
                Socket.ReconnectTo(ip);

                //Store in the cache the value for the next step
                Cache.Add(PrefixMessage.ServerSelection.SelectServer, message.Substring(14, 7));
            }
            else if (message.StartsWith(PrefixMessage.ServerSelection.SwitchSocketOk))
            {
                Socket.Send(PrefixMessage.ServerSelection.ServerIdentification + Cache[PrefixMessage.ServerSelection.SelectServer] + CharNewLine + CharNull);
            }
            else if (message.StartsWith(PrefixMessage.ServerSelection.ServerIdentification))
            {
                Socket.Send("Ai07ZMr3g5oEkTN0" + CharNewLine + CharNull + "AL" + CharNewLine + CharNull + "Af" + CharNewLine + CharNull);
            }
            else if (message.StartsWith(PrefixMessage.ServerSelection.PlaceQueue))
            {

                string place = message.Remove(0, PrefixMessage.ServerSelection.PlaceQueue.Length);
                Program.log.Info("Place in the queue: "+place);

                //If pace in the queue change
                if (!Cache.ContainsKey("place") || Cache["place"] != place)
                {
                    Cache["delay"] = ((Int32)1000).ToString();
                    //Caching the place
                    Cache["place"] = place;
                }
                else
                {
                    Cache["delay"] = (Int32.Parse(Cache["delay"])*2).ToString();
                }


                Thread.Sleep(Int32.Parse(Cache["delay"]));

                Socket.Send("Af" + CharNewLine + CharNull);
            }
            else if (message.StartsWith(PrefixMessage.CharacterSelection.PersoInfo))
            {
                State = new CharacterSelectionState(this);
                PersoSelection(message);
            }
            return 0;
        }

        public int PersoSelection(string message)
        {
            if (message.StartsWith(PrefixMessage.CharacterSelection.PersoInfo))
            {
                Character.ParseMessageFromServer(message);
                Socket.Send("AS"+Character.Id+CharNewLine+CharNull);
            }
            else if (message.StartsWith(PrefixMessage.CharacterSelection.PersoConnected))
            {
                Socket.Send("GC1" + CharNewLine + CharNull);
            }
            else if (message.StartsWith(PrefixMessage.CharacterSelection.MapDetails))
            {
                State = new InGameState(this);
                Socket.Send(PrefixMessage.MapLoading.AskInfo+CharNewLine+CharNull);
            }
            return 0;
        }

        public int InGame(string message)
        {
            if (message.StartsWith(PrefixMessage.MapLoading.GetInfo))
            {
                var data = message.Split('|');
                var idMap = data[1];
                var botNumber = data[2];
                var key = data[3];

                var unpackMapData = _mapLoader.LoadMap(idMap, botNumber, key);
            }
            return 0;
        }

        private void ConnectCharacter()
        {
            Program.log.Info("Account connection attempt ....");
            try
            {
                var encryptedPassword = Account.EncryptionPassword();
                Socket.Send("1.29.1" + CharNewLine + CharNull);
                Socket.Send(Account.Pseudo + CharNewLine + encryptedPassword + CharNewLine + CharNull + "Af" + CharNewLine + CharNull);
            }
            catch (NullReferenceException e)
            {
                Program.log.Info("Secret key not received from the server");
                Program.log.Error(e.ToString());
            }
        }

        private void SelectServer()
        {
            Program.log.Info("Server selection ...");
            Socket.Send(PrefixMessage.ServerSelection.SelectServer + IDSERVER + CharNewLine + CharNull);
        }
    }
}
