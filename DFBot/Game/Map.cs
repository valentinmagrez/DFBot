using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Game
{
    /***
     * Define the map object
     **/
    public class Map
    {
        #region attributes
        private List<MonsterGroup> _monsterGroups;
        
        private List<Harvestable> _harvestables;
        #endregion

        #region properties
        public List<Harvestable> Harvestables
        {
            get { return _harvestables; }
        }

        public List<MonsterGroup> MonsterGroups
        {
            get { return _monsterGroups; }
        }
        #endregion
    }
}
