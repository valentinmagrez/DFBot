using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFBot.Network.Message;

namespace DFBot.Lib
{
    public class SeverIPDecrypt
    {
        public static string Decrypt(string message)
        {
            var ipCrypted = message.Substring(3, 8).ToCharArray();

            var i = 0;
            var times = 0;
            var ipUncrypted = string.Empty;

            while (i<8)
            {
                i++;
                times++;
                var dat1 = ipCrypted[i-1] - 48;
                i++;
                var dat2 = ipCrypted[i - 1] - 48;
                var dat3 = ((dat1 & 15) << 4 | dat2 & 15).ToString();
                ipUncrypted += dat3;
                if (i < 8)
                    ipUncrypted += ".";
            }

            return ipUncrypted;
        }

        //While(i< 8)
        //i = i + 1
        //fois = fois + 1
        //Dim dat1 As Integer = Asc(Mid(ipCrypt, i, 1)) - 48
        //i = i + 1
        //Dim dat2 As Integer = Asc(Mid(ipCrypt, i, 1)) - 48
        //Dim Dat3 As String = Str(((dat1 And 15) << 4 Or dat2 And 15))
        //If(fois > 1) Then
        //    ipServeurJeu = ipServeurJeu + Mid(Dat3, 2)
        //Else
        //    ipServeurJeu = ipServeurJeu + Dat3
        //End If
        //If(i< 8) Then
        //    ipServeurJeu = ipServeurJeu + "."
        //End If
        //End While
    }
}
