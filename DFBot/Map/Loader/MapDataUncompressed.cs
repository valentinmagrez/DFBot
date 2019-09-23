using System;

namespace DFBot.Map.Loader
{
    public class MapDataUncompressed
    {
        private readonly string _mapId;
        private readonly string _coordinateX;
        private readonly string _coordinateY;
        private readonly string _mapData;

        public MapDataUncompressed(string mapId, string coordinateX, string coordinateY, string mapData)
        {
            _mapId = mapId;
            _coordinateX = coordinateX;
            _coordinateY = coordinateY;
            _mapData = mapData;
        }

        public static MapDataUncompressed CreateFromStringSwfData(string swfData)
        {
            var mapData = swfData.Split('\'')[29];

            var splitStringForMapInfo = new[] { "push" };
            var mapId = swfData.Split(splitStringForMapInfo, StringSplitOptions.None)[8].Split(' ')[1];
            var mapX = swfData.Split(splitStringForMapInfo, StringSplitOptions.None)[10].Split(' ')[1]; ;
            var mapY = swfData.Split(splitStringForMapInfo, StringSplitOptions.None)[12].Split(' ')[1]; ;

            return new MapDataUncompressed(mapId, mapX, mapY, mapData);
        }

        public string ToTextFile()
        {
            return _mapId + "|" + _mapData + "|" + _coordinateX + "|" + _coordinateY;
        }
    }
}