using System;
using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.Utilities.Helpers;
using Newtonsoft.Json;

namespace MusicPlayer.Core.Albums
{
    public class AlbumDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy")]
        public DateTime Year { get; set; }

        public string[] ImagePaths { get; set; }

        public List<SongInfoDto> SongInfos { get; set; }

        public TimeSpan TotalDuration
        {
            get
            {
                var duration = new TimeSpan();
                if (SongInfos.Any())
                {
                    SongInfos.ForEach(si => duration += duration.Add(si.Duration));
                }

                return duration;
            }
        }
    }
}
