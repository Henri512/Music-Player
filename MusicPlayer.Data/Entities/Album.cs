using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MusicPlayer.Data.Entities
{
    public class Album : BaseEntity
    {
        public Album()
        {
            SongInfos = new List<SongInfo>();
        }

        [StringLength(300)]
        [Required]
        public string Name { get; set; }

        public DateTime Year { get; set; }

        [StringLength(200)]
        public string Author { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }

        public List<SongInfo> SongInfos { get; set; }

        [NotMapped]
        public TimeSpan TotalDuration
        {
            get
            {
                var duration = new TimeSpan();
                if(SongInfos.Any())
                {
                    SongInfos.ForEach(si =>  duration += duration.Add(si.Duration));
                }

                return duration;
            }
        }
    }
}
