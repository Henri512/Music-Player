using System;
using System.Collections.Generic;
using MusicPlayer.Utilities.Helpers;
using Newtonsoft.Json;

namespace MusicPlayer.Model.Models
{
    public class AlbumModel
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Author { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy")]
        public DateTime Year { get; set; }

        public string[] ImagePaths { get; set; }

        public List<SongInfoModel> SongInfos { get; set; }
    }
}
