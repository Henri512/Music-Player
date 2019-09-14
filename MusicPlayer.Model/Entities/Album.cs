using MusicPlayer.Utilities.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Model.Entities
{
    public class Album : BaseEntity
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy")]
        public DateTime Year { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        public List<SongInfo> SongInfos { get; set; }
    }
}
