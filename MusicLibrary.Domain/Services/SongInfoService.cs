﻿using System;
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
    public class SongInfoService : GlobalService, ISongInfoService
    {
        private readonly ISongInfoRepository _songInfoRepository;

        public SongInfoService(
            ISongInfoRepository songInfoRepository,
            IMapper mapper,
            IConfiguration configuration)
            : base(mapper, configuration)
        {
            _songInfoRepository = songInfoRepository;
        }

        public IEnumerable<SongInfoModel> GetSongInfos(bool includeAlbum)
        {
            var songInfos = includeAlbum ? _songInfoRepository.GetSongInfos().Include(s => s.Album) : _songInfoRepository.GetSongInfos();

            var songInfoModels = _mapper
                .Map<IEnumerable<SongInfoModel>>(songInfos.ToList())
                .ToList();
            songInfoModels
                .ForEach(
                s =>
                {
                    s.BlobFileReference = GetBlobFileUriLocation(s.RelativePath);

                    s.AlbumImagePath = s.AlbumImagePath != null
                    && s.AlbumImagePath.Any() ?
                        GetAlbumImagesUrls(s.AlbumImagePath)
                        : new string[] { _defaultAlbumLogoImageUrl };
                });

            return songInfoModels;
        }

        public SongInfoModel GetSongInfoById(int id, bool includeAlbum)
        {
            var songInfo = includeAlbum ?
                _songInfoRepository.GetSongInfoById(id).Include(s => s.Album)
                : _songInfoRepository.GetSongInfoById(id);
            var songInfoModel = _mapper.Map<SongInfoModel>(
                songInfo.FirstOrDefault());
            songInfoModel.BlobFileReference = GetBlobFileUriLocation(
                songInfoModel.RelativePath);

            songInfoModel.AlbumImagePath =
                songInfoModel.AlbumImagePath != null
                && songInfoModel.AlbumImagePath.Any() ?
                     GetAlbumImagesUrls(songInfoModel.AlbumImagePath)
                    : new string[] { _defaultAlbumLogoImageUrl };

            return songInfoModel;
        }

        public SongInfoModel AddSongInfo(SongInfoModel songInfoModel)
        {
            var songInfo = _mapper.Map<SongInfo>(songInfoModel);
            return _mapper.Map<SongInfoModel>(
                _songInfoRepository.AddSongInfo(songInfo));
        }

        public bool SongPlayed(int id)
        {
            var songInfo = _songInfoRepository
                .GetSongInfoById(id)
                .FirstOrDefault();
            if (songInfo == null)
            {
                throw new Exception("Song does not exist!");
            }

            songInfo.TimesPlayed++;
            _songInfoRepository.UpdateSongInfo(songInfo);
            return _songInfoRepository.Save() > 0;
        }
    }
}
