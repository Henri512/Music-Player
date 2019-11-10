using System;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Core.SongInfos
{
    public class SongInfo : BaseEntity
    {
        public int? AlbumId { get; set; }

        public string Name { get; set; }

        public int TrackNo { get; set; }

        public string Author { get; set; }

        public TimeSpan Duration { get; set; }

        public string Genre { get; set; }

        public string BitRate { get; set; }

        public int TimesPlayed { get; set; }

        public string RelativePath { get; set; }

        public string Extension { get; set; }

        public Album Album { get; set; }

        public SongInfoDto ToDto()
        {
            return new SongInfoDto
            {
                Name = Name,
                Author = Author,
                Duration = Duration,
                BitRate = BitRate,
                Extension = Extension,
                Genre = Genre,
                RelativePath = RelativePath,
                TimesPlayed = TimesPlayed,
                AlbumName = Album?.Name,
                AlbumYear = Album?.Year,
                AlbumId = AlbumId,
                AlbumImagePath = string.IsNullOrEmpty(Album?.ImagePath) ?
                    null : Album?.ImagePath.Split(';')
            };
        }

        public static SongInfo ToEntity(SongInfoDto songInfoDto)
        {
            return new SongInfo
            {
                Name = songInfoDto.Name,
                Author = songInfoDto.Author,
                Duration = songInfoDto.Duration,
                BitRate = songInfoDto.BitRate,
                Extension = songInfoDto.Extension,
                Genre = songInfoDto.Genre,
                RelativePath = songInfoDto.RelativePath,
                TimesPlayed = songInfoDto.TimesPlayed,
                AlbumId = songInfoDto.AlbumId
            };
        }
    }
}
