namespace DFBot.Map
{
    public class MapLoader
    {
        private MapUnpacker _mapUnpacker;
        private MapDecipherer _mapDecipherer;

        public MapLoader()
        {
            _mapUnpacker = new MapUnpacker();
            _mapDecipherer = new MapDecipherer();
        }

        public object LoadMap(string idMap, string botNumber, string key)
        {
            var cypherMapData = _mapUnpacker.GetUnpackedMapData(idMap, botNumber);
            _mapDecipherer.DecypherMapData(cypherMapData, key);

            return null;
        }
    }
}
