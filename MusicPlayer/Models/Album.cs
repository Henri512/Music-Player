using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MusicPlayer.Models
{
    public class Album
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public DateTime Year { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        public List<SongInfo> SongInfos { get; set; }
    }
}
