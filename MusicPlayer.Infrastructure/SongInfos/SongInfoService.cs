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
            var songInfos = includeAlbum ?
                _songInfoGenericRepository.Get()
                    .Include(s => s.Album) 
                : _songInfoGenericRepository.Get();

            var songInfoModels = songInfos.ToList().Select(s => s.ToDto()).ToList();
            songInfoModels
                .ForEach(
                s =>
                {
                    s.BlobFileReference = GetBlobFileUriLocation(s.RelativePath);

                    s.AlbumImagePath = s.AlbumImagePath != null
                    && s.AlbumImagePath.Any() ?
                        GetAlbumImagesUrls(s.AlbumImagePath)
                        : new[] { _defaultAlbumLogoImageUrl };
                });

            return songInfoModels;
        }

        public SongInfoDto GetSongInfoById(int id, bool includeAlbum)
        {
            var songInfo = includeAlbum ?
                _songInfoGenericRepository.GetById(id).Include(s => s.Album)
                : _songInfoGenericRepository.GetById(id);
            var songInfoModel = songInfo.FirstOrDefault()?.ToDto();
            songInfoModel.BlobFileReference = GetBlobFileUriLocation(
                songInfoModel.RelativePath);

            songInfoModel.AlbumImagePath =
                songInfoModel.AlbumImagePath != null
                && songInfoModel.AlbumImagePath.Any() ?
                     GetAlbumImagesUrls(songInfoModel.AlbumImagePath)
                    : new string[] { _defaultAlbumLogoImageUrl };

            return songInfoModel;
        }

        public SongInfoDto AddSongInfo(SongInfoDto songInfoDto)
        {
            var songInfo = SongInfo.ToEntity(songInfoDto);
            return _songInfoGenericRepository.Add(songInfo).ToDto();
        }

        public bool SongPlayed(int id)
        {
            var songInfo = _songInfoGenericRepository
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
