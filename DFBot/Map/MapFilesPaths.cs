using System.IO;

namespace DFBot.Map
{
    public class MapFilesPaths
    {
        private readonly string _mapId;
        private readonly string _indice;

        private string MapSwfDirectoryPath => @"C:\Program Files (x86)\ThinkPad\Dofus\data\maps";
        private string MapsUnpackedDirectoryPath => Directory.GetCurrentDirectory() + "/maps";
        private string MapFileName => $"{_mapId}_{_indice}X";

        public MapFilesPaths(string mapId, string indice)
        {
            _mapId = mapId;
            _indice = indice;
        }

        public string SwfFilePath => $"{MapSwfDirectoryPath}/{MapFileName}.swf";
        public string UnpackedFilePath => $"{MapsUnpackedDirectoryPath}/{MapFileName}.txt";

        public void CreateLocalMapDirectoryIfNotExist()
        {
            if (!Directory.Exists(MapsUnpackedDirectoryPath))
                Directory.CreateDirectory(MapsUnpackedDirectoryPath);
        }
    }
}