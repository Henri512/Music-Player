using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public class SongInfoService : GlobalService, ISongInfoService
    {
        private readonly IGenericRepository<SongInfo> _songInfoGenericRepository;

        public SongInfoService(
            ISongInfoRepository songInfoRepository,
            IConfiguration configuration)
            : base(configuration)
        {
            _songInfoGenericRepository =
                songInfoRepository as IGenericRepository<SongInfo>;
        }

        public IEnumerable<SongInfoDto> GetSongInfos(bool includeAlbum)
        {
            IQueryable<SongInfo> query = includeAlbum ?
                _songInfoGenericRepository.Get()
                    .Include(s => s.Album) 
                : _songInfoGenericRepository.Get();

            List<SongInfoDto> songInfoModels = query.ToList().Select(s => s.ToDto()).ToList();
            songInfoModels
                .ForEach(
                s =>
                {
                    s.BlobFileReference = GetBlobFileUriLocation(s.RelativePath);

                    s.AlbumImagePath = s.AlbumImagePath != null
                    && s.AlbumImagePath.Any() ?
                        GetAlbumImagesUrls(s.AlbumImagePath)
                        : new[] { DefaultAlbumLogoImageUrl };
                });

            return songInfoModels;
        }

        public SongInfoDto GetSongInfoById(int id, bool includeAlbum)
        {
            IQueryable<SongInfo> query = includeAlbum ?
                _songInfoGenericRepository.GetById(id).Include(s => s.Album)
                : _songInfoGenericRepository.GetById(id);
            SongInfoDto songInfoDto = query.FirstOrDefault()?.ToDto();
            songInfoDto.BlobFileReference = GetBlobFileUriLocation(
                songInfoDto.RelativePath);

            songInfoDto.AlbumImagePath =
                songInfoDto.AlbumImagePath != null
                && songInfoDto.AlbumImagePath.Any() ?
                     GetAlbumImagesUrls(songInfoDto.AlbumImagePath)
                    : new [] { DefaultAlbumLogoImageUrl };

            return songInfoDto;
        }

        public SongInfoDto AddSongInfo(SongInfoDto songInfoDto)
        {
            SongInfo songInfo = SongInfo.ToEntity(songInfoDto);
            return _songInfoGenericRepository.Add(songInfo).ToDto();
        }

        public bool SongPlayed(int id)
        {
            SongInfo songInfo = _songInfoGenericRepository
                .GetById(id)
                .FirstOrDefault();
            if (songInfo == null)
            {
                throw new Exception("Song does not exist!");
            }

            songInfo.TimesPlayed++;
            _songInfoGenericRepository.Update(songInfo);
            return _songInfoGenericRepository.Save() > 0;
        }
    }
}
