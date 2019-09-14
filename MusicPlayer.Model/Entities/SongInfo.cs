using System;
using System.ComponentModel.DataAnnotations;
using MusicPlayer.Utilities.Helpers;
using Newtonsoft.Json;

namespace MusicPlayer.Model.Entities
{
    public class SongInfo : BaseEntity
    {
        public int? AlbumId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [JsonConverter(typeof(CustomTimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        [StringLength(30)]
        public string Genre { get; set; }

        public int BitRate { get; set; }

        public int TimesPlayed { get; set; }

        [StringLength(255)]
        public string PhysicalPath { get; set; }

        [StringLength(5)]
        public string Extension { get; set; }

        public Album Album { get; set; }
    }
}
