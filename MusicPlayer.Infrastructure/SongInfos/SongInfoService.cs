using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.Infrastructure.Blobs;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public class SongInfoService : ISongInfoService
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly IGenericRepository<SongInfo> _songInfoGenericRepository;

        public SongInfoService(
            ISongInfoRepository songInfoRepository,
            IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
            _songInfoGenericRepository =
                songInfoRepository as IGenericRepository<SongInfo>;
        }

        public IEnumerable<SongInfo> GetSongInfos(bool includeAlbum)
        {
            IQueryable<SongInfo> query = includeAlbum ?
                _songInfoGenericRepository.Get()
                    .Include(s => s.Album) 
                : _songInfoGenericRepository.Get();

            List<SongInfo> songInfos = query.ToList();
            //songInfoModels
            //songInfoModels
            //    .ForEach(
            //    s =>
            //    {
            //        s.BlobFileReference = GetBlobFileUriLocation(s.RelativePath);

            //        s.AlbumImagePath = s.AlbumImagePath != null
            //        && s.AlbumImagePath.Any() ?
            //            GetAlbumImagesUrls(s.AlbumImagePath)
            //            : new[] { DefaultAlbumLogoImageUrl };
            //    });

            return songInfos;
        }

        public SongInfo GetSongInfoById(int id, bool includeAlbum)
        {
            IQueryable<SongInfo> query = includeAlbum ?
                _songInfoGenericRepository.GetById(id).Include(s => s.Album)
                : _songInfoGenericRepository.GetById(id);
            SongInfo songInfo = query.FirstOrDefault();
            //songInfoDto.BlobFileReference = GetBlobFileUriLocation(
            //    songInfoDto.RelativePath);

            //songInfoDto.AlbumImagePath =
            //    songInfoDto.AlbumImagePath != null
            //    && songInfoDto.AlbumImagePath.Any() ?
            //         GetAlbumImagesUrls(songInfoDto.AlbumImagePath)
            //        : new [] { DefaultAlbumLogoImageUrl };

            return songInfo;
        }

        public SongInfo AddSongInfo(SongInfoDto songInfoDto)
        {
            SongInfo songInfo = SongInfo.ToEntity(songInfoDto);
            return _songInfoGenericRepository.Add(songInfo);
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
