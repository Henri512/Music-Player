using System;
using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Core.Albums
{
    public class Album : BaseEntity
    {
        public Album()
        {
            SongInfos = new List<SongInfo>();
        }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public string Author { get; set; }

        public string ImagePath { get; set; }

        public List<SongInfo> SongInfos { get; set; }
        
        public AlbumDto ToDto()
        {
            return new AlbumDto
            {
                Id = Id,
                Name=Name,
                Author = Author,
                Year = Year,
                SongInfos = SongInfos.Any() ? SongInfos.Select(s => s.ToDto()).ToList() : new List<SongInfoDto>(),
                ImagePaths = string.IsNullOrEmpty(ImagePath) ?
                    null : ImagePath.Split(';')
            };
        }

        public static Album ToEntity(AlbumDto albumDto)
        {
            return new Album
            {
                Name = albumDto.Name,
                Author = albumDto.Author,
                Year = albumDto.Year,
                ImagePath = albumDto.ImagePaths != null && albumDto.ImagePaths.Any() ? string.Join(';', albumDto.ImagePaths) : string.Empty
            };
        }
    }
}
