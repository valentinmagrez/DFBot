using System;
using System.IO;
using SwfDotNet.IO;
using SwfDotNet.IO.ByteCode;
using SwfDotNet.IO.Tags;

namespace DFBot.Map
{
    public class MapLoader
    {
        private string MapSwfDirectoryPath => @"C:\Program Files (x86)\ThinkPad\Dofus\data\maps";
        private string MapsConvertedDirectoryPath => Directory.GetCurrentDirectory() + "/maps";

        public void LoadMap(string mapId, string indice, string keyDecipher)
        {
            CreateLocalMapDirectoryIfNotExist();
            var mapPath = GetMapFullFilePath(mapId, indice);
            UnpackSwfFile(mapPath);
        }

        private string GetMapFullFilePath(string mapId, string indice)
        {
            var mapFileName = $"{mapId}_{indice}X.swf";
            return $"{MapSwfDirectoryPath}/{mapFileName}";
        }

        private void CreateLocalMapDirectoryIfNotExist()
        {
            if (!Directory.Exists(MapsConvertedDirectoryPath))
                Directory.CreateDirectory(MapsConvertedDirectoryPath);
        }

        public bool UnpackSwfFile(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            var swfReader = new SwfReader(filePath);
            var swfInfo = swfReader.ReadSwf();
            var tags = swfInfo.Tags.GetEnumerator();

            var uncompressedSwfData = string.Empty;
            while (tags.MoveNext())
            {
                var tag = (BaseTag) tags.Current;
                if(tag?.ActionRecCount == 0)
                    continue;

                var nextTags = tag.GetEnumerator();
                while (nextTags.MoveNext())
                {
                    var decompiler = new Decompiler(swfInfo.Version);
                    var actions = decompiler.Decompile((byte[])nextTags.Current);
                    foreach (var action in actions)
                    {
                        uncompressedSwfData += action + Environment.NewLine;
                    }

                    var mapDataUncompressed = MapDataUncompressed.CreateFromStringSwfData(uncompressedSwfData);

                    var mapTextFilename = Path.GetFileNameWithoutExtension(filePath);
                    var mapConvertedFullPath = MapsConvertedDirectoryPath + "/" + mapTextFilename + ".txt";
                    using (var writer = new StreamWriter(mapConvertedFullPath))
                        writer.Write(mapDataUncompressed.ToTextFile());
                }
            }

            return true;
        }
    }

    public class MapDataUncompressed
    {
        private string _mapId;
        private string _coordinateX;
        private string _coordinateY;
        private string _mapData;

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
