using System;
using System.Linq;
using static System.Int32;

namespace DFBot
{
    public class Character
    {
        public string Pseudo { get; set; }

        public int Id { get; private set; }

        public int Level { get; set; }

        public Character(string pseudo)
        {
            Pseudo = pseudo;
        }

        public void ParseMessageFromServer(string message)
        {
            var messagesFromServer = message.Split('|');

            //Retrieve the data of the choosen character
            var characterData = messagesFromServer
                                        .First(x => x.Split(';').Length > 1 
                                                    && x.Contains(Pseudo))
                                        .Split(';');

            try {
                Id = Parse(characterData[0]);
                Level = Parse(characterData[2]);
            }
            catch(IndexOutOfRangeException e)
            {
                Program.log.Info("Impossible de se connecter au personnage. Vérifiez que le pseudo ("+Pseudo+") est le bon.");
                Program.log.Error(e.ToString());
            }
        }
    }
}
