using System;
using System.IO;
using System.Text.RegularExpressions;
using MusicPlayer.Utilities.Helpers;
using Newtonsoft.Json;

namespace MusicPlayer.Model.Models
{
    public class SongInfoModel
    {
        public int Id { get; private set; }

        public int? AlbumId { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        [JsonConverter(typeof(CustomTimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        public string Genre { get; set; }

        public string BitRate { get; set; }

        public int TimesPlayed { get; set; }

        public string RelativePath { get; set; }

        public string Extension { get; set; }
        
        public string AlbumName { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy")]
        public DateTime AlbumYear { get; set; }

        public string[] AlbumImagePath { get; set; }
        
        public string AlbumWithoutAYear
        {
            get
            {
                string regex = "(\\(.*\\))|(\".*\")|('.*')|(\\(.*\\))";
                string album = Regex.Replace(AlbumName, regex, "");
                return album;
            }
        }

        public string BlobFileReference { get; set; }
    }
}
