using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot
{
    public class Perso
    {
        #region attributes from config
        /// <summary>
        /// Pseudo of the character
        /// </summary>
        private string _pseudo;

        public string Pseudo
        {
            get { return _pseudo; }
            set { _pseudo = value; }
        }

        /// <summary>
        /// Game's version where the character belongs
        /// </summary>
        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// character server
        /// </summary>
        private string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        #endregion

        #region attributes from server
        /// <summary>
        /// Id of the character
        /// </summary>
        private int _id;

        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// level of the character
        /// </summary>
        private int _level;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        #endregion

        public Perso(string pseudo)
        {
            _pseudo = pseudo;
        }
        public void ParseMessageFromServer(string message)
        {
            string[] messagesFromServer = message.Split('|');

            //Retrieve the data of the choosen character
            string[] characterData = messagesFromServer
                                        .Where(x => x.Split(';').Length > 1 
                                            && x.Contains(_pseudo))
                                        .First()
                                        .Split(';');

            try {

                _id = Int32.Parse(characterData[0]);
                _level = Int32.Parse(characterData[2]);
            }
            catch(IndexOutOfRangeException e)
            {
                Program.log.Info("Impossible de se connecter au personnage. Vérifiez que le pseudo ("+_pseudo+") est le bon.");
                Program.log.Error(e.ToString());
            }
        }
    }
}
