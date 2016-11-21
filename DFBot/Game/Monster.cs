using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Game
{
    /***
     * Describe a monster
     * */
    public class Monster
    {
        #region attributes
        private int _id;

        private int _lvl;
        #endregion

        #region properties
        public int Level
        {
            get { return _lvl; }
            set { _lvl = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion
    }
}
