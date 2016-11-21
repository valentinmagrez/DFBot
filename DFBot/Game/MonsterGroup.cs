using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Game
{
    /***
     * Describe a group monsters
     * */
    public class MonsterGroup
    {
        #region attributes
        private List<Monster> _monsters;
        #endregion
        #region properties
        public List<Monster> Monsters
        {
            get { return _monsters; }
        }
        #endregion
    }
}
