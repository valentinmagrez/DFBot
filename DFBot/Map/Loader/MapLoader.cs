using System;
using System.IO;
using SwfDotNet.IO;
using SwfDotNet.IO.ByteCode;
using SwfDotNet.IO.Tags;

namespace DFBot.Map.Loader
{
    public class MapLoader
    {
        public void LoadMap(string mapId, string indice)
        {
            var mapFilesPaths = new MapFilesPaths(mapId, indice);
            mapFilesPaths.CreateLocalMapDirectoryIfNotExist();

            UnpackSwfFileInLocalMapDirectory(mapFilesPaths);
        }

        public bool UnpackSwfFileInLocalMapDirectory(MapFilesPaths mapFilesPaths)
        {
            if (!File.Exists(mapFilesPaths.SwfFilePath))
                return false;

            var swfReader = new SwfReader(mapFilesPaths.SwfFilePath);
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
                        uncompressedSwfData += action + Environment.NewLine;

                    var mapDataUncompressed = MapDataUncompressed.CreateFromStringSwfData(uncompressedSwfData);

                    using (var writer = new StreamWriter(mapFilesPaths.UnpackedFilePath))
                        writer.Write(mapDataUncompressed.ToTextFile());
                }
            }

            return true;
        }
    }
}
