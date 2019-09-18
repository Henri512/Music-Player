using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public SongInfoService(ISongInfoRepository songInfoRepository, IMapper mapper)
        {
            _songInfoRepository = songInfoRepository;
            _mapper = mapper;
        }

        public IEnumerable<SongInfoModel> GetSongInfos(bool includeAlbum)
        {
            var songInfos = includeAlbum ? _songInfoRepository.GetSongInfos().Include(s => s.Album) : _songInfoRepository.GetSongInfos();
            return _mapper.Map<IEnumerable<SongInfoModel>>(songInfos.ToList());
        }

        public SongInfoModel GetSongInfoById(int id, bool includeAlbum)
        {
            var songInfo = includeAlbum ? _songInfoRepository.GetSongInfoById(id).Include(s => s.Album) : _songInfoRepository.GetSongInfoById(id);
            return _mapper.Map<SongInfoModel>(songInfo.FirstOrDefault());
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
    }
}
