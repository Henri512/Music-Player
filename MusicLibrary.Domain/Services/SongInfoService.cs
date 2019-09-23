using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;

namespace MusicPlayer.Domain.Services
{
    public class SongInfoService : ISongInfoService
    {
        private readonly ISongInfoRepository _songInfoRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string _blobStorageUrl;
        private readonly string _defaultAlbumLogoImageUrl;

        public SongInfoService(
            ISongInfoRepository songInfoRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _songInfoRepository = songInfoRepository;
            _mapper = mapper;
            _configuration = configuration;
            var blobSection = _configuration.GetSection("BlobStorage");
            _blobStorageUrl = blobSection
                .GetValue<string>("SongsBlobUrl") ?? string.Empty;
            _defaultAlbumLogoImageUrl = blobSection
                .GetValue<string>("DefaultAlbumLogoImageUrl") ?? string.Empty;
        }

        public IEnumerable<SongInfoModel> GetSongInfos(bool includeAlbum)
        {
            var songInfos = includeAlbum ? _songInfoRepository.GetSongInfos().Include(s => s.Album) : _songInfoRepository.GetSongInfos();
            
            var songInfoModels = _mapper
                .Map<IEnumerable<SongInfoModel>>(songInfos.ToList())
                .ToList();
            songInfoModels
                .ForEach(
                s => {
                    s.BlobFileReference =
                    new Uri(_blobStorageUrl +
                        s.FullName.Replace('\\', '/')).OriginalString;
                    s.AlbumImagePath = string.IsNullOrEmpty(s.AlbumImagePath)?
                    _defaultAlbumLogoImageUrl
                    : s.AlbumImagePath;
                    });

            return songInfoModels;
        }

        public SongInfoModel GetSongInfoById(int id, bool includeAlbum)
        {
            var songInfo = includeAlbum ? _songInfoRepository.GetSongInfoById(id).Include(s => s.Album) : _songInfoRepository.GetSongInfoById(id);
            var songInfoModel = _mapper.Map<SongInfoModel>(songInfo.FirstOrDefault());
            songInfoModel.BlobFileReference = 
                new Uri(_blobStorageUrl +
                        songInfoModel.FullName.Replace('\\', '/'))
                .OriginalString;
            songInfoModel.AlbumImagePath = string
                .IsNullOrEmpty(songInfoModel.AlbumImagePath)?
                    _defaultAlbumLogoImageUrl
                    : songInfoModel.AlbumImagePath;

            return songInfoModel;
        }

        public SongInfoModel AddSongInfo(SongInfoModel songInfoModel)
        {
            var songInfo = _mapper.Map<SongInfo>(songInfoModel);
            return _mapper.Map<SongInfoModel>(_songInfoRepository.AddSongInfo(songInfo));
        }

        public bool SongPlayed(int id)
        {
            var songInfo = _songInfoRepository.GetSongInfoById(id).FirstOrDefault();
            if (songInfo == null)
            {
                throw  new Exception("Song does not exist!");
            }

            songInfo.TimesPlayed++;
            _songInfoRepository.UpdateSongInfo(songInfo);
            return _songInfoRepository.Save() > 0;
        }

        private string GetUrl(string songFullName)
        {
            return new Uri(_blobStorageUrl +
                        songFullName.Replace('\\', '/'))
                .OriginalString;
        }
    }
}
