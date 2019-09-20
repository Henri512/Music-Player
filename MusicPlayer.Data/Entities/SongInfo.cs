using System;
using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Data.Entities
{
    public class SongInfo : BaseEntity
    {
        public int? AlbumId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int TrackNo { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public TimeSpan Duration { get; set; }

        [StringLength(30)]
        public string Genre { get; set; }

        [StringLength(20)]
        public string BitRate { get; set; }

        public int TimesPlayed { get; set; }

        [StringLength(255)]
        public string RelativePath { get; set; }

        [StringLength(5)]
        public string Extension { get; set; }

        public Album Album { get; set; }
    }
}
