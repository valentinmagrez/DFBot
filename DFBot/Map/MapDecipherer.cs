using System;
using System.Net;

namespace DFBot.Map
{
    public class MapDecipherer
    {
        public string DecypherMapData(string mapData, string key)
        {
            var decypheredKey = DecypherKey(key);
            return DecypherMap(mapData, decypheredKey);
        }

        private string DecypherKey(string key)
        {
            var result = string.Empty;

            for (var i = 0; i < key.Length; i += 2)
            {
                result +=  (char)Convert.ToInt64(key.Substring(i, 2), 16);
            }

            return WebUtility.UrlDecode(result);
        }

        private string DecypherMap(string mapData, string decypheredKey)
        {
            var result = string.Empty;
            var checksum = Convert.ToInt64(Checksum(decypheredKey), 16) * 2;

            var j = 0;
            for (var i = 0; i < mapData.Length; i += 2)
            {
                var a = Convert.ToInt64(mapData.Substring(i, 2), 16);
                var b = (int)decypheredKey[(int) (j + checksum) % decypheredKey.Length];
                result += (char)(a ^ b);
                j++;
            }

            return WebUtility.UrlDecode(result);
        }

        public string Checksum(string value)
        {
            var result = 0;
            foreach (var character in value)
                result += character % 16;

            return (result % 16).ToString("X");
        }
    }
}