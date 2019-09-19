using System;

namespace DFBot
{
    public class Account
    {
        public string Pseudo { get; set; }

        public string Password { get; set; }

        public string Key { get; set; }

        public Account(string pseudo, string password)
        {
            Pseudo = pseudo;
            Password = password;
        }

        /// <summary>
        /// Encrypt the password for sending to the server
        /// </summary>
        /// <returns></returns>
        public string EncryptionPassword()
        {
            Program.log.Info("Password encryption");

            if (Key!=null)
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
                int num2 = Password.Length;
                int Start = num1;
                while (Start < num2)
                {
                    int num3 = (int)Password[Start];
                    int num4 = (int)Key[Start];
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
    }
}
