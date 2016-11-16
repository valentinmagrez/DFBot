using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot
{
    public class Account
    {
        #region attributes
        /// <summary>
        /// Identifier for the connection
        /// </summary>
        private string _pseudo;

        public string Pseudo
        {
            get { return _pseudo; }
            set { _pseudo = value; }
        }

        /// <summary>
        /// Password for the connection
        /// </summary>
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Key provided by the server to log in
        /// </summary>
        private string _key;

        public string Key
        {
            set { _key = value; }
        }
        #endregion

        public Account(string pseudo, string password)
        {
            _pseudo = pseudo;
            _password = password;
        }

        #region public methods
        /// <summary>
        /// Encrypt the password for sending to the server
        /// </summary>
        /// <returns></returns>
        public string EncryptionPassword()
        {
            Program.log.Info("Password encryption");

            if (_key!=null)
            {
                //TODO clean this code
                string str1 = "#1";
                string[] strArray = new string[64]
                {
                    "a",
                    "b",
                    "c",
                    "d",
                    "e",
                    "f",
                    "g",
                    "h",
                    "i",
                    "j",
                    "k",
                    "l",
                    "m",
                    "n",
                    "o",
                    "p",
                    "q",
                    "r",
                    "s",
                    "t",
                    "u",
                    "v",
                    "w",
                    "x",
                    "y",
                    "z",
                    "A",
                    "B",
                    "C",
                    "D",
                    "E",
                    "F",
                    "G",
                    "H",
                    "I",
                    "J",
                    "K",
                    "L",
                    "M",
                    "N",
                    "O",
                    "P",
                    "Q",
                    "R",
                    "S",
                    "T",
                    "U",
                    "V",
                    "W",
                    "X",
                    "Y",
                    "Z",
                    "0",
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9",
                    "-",
                    "_"
                    };
                int num1 = 0;
                int num2 = _password.Length;
                int Start = num1;
                while (Start < num2)
                {
                    int num3 = (int)_password[Start];
                    int num4 = (int)_key[Start];
                    int num5 = checked((int)Math.Floor(unchecked((double)num3 / 16.0)));

                    int num6 = num3 % 16;
                    string str2 = strArray[checked(num5 + num4) % strArray.Length];
                    string str3 = strArray[checked(num6 + num4) % strArray.Length];
                    str1 = str1 + str2 + str3;
                    checked { ++Start; }
                }

                Program.log.Info("Password encrypted succesfully: "+str1);

                return str1;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        #endregion

        #region private methods


        #endregion
    }
}
