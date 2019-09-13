﻿using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models;
using MusicPlayer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.Services
{
    public class SongInfoService : ISongInfoService
    {
        private readonly ISongInfoRepository _songInfoRepository;

        public SongInfoService(ISongInfoRepository songInfoRepository)
        {
            _songInfoRepository = songInfoRepository;
        }

        public IEnumerable<SongInfo> GetSongInfos(bool includeAlbum)
        {
            var songInfos = includeAlbum ? _songInfoRepository.GetSongInfos().Include(s => s.Album) : _songInfoRepository.GetSongInfos();
            return songInfos.ToList();
        }

        public SongInfo GetSongInfoById(int id, bool includeAlbum)
        {
            var songInfo = includeAlbum ? _songInfoRepository.GetSongInfoById(id).Include(s => s.Album) : _songInfoRepository.GetSongInfoById(id);
            return songInfo.FirstOrDefault();
        }

        public SongInfo AddSongInfo(SongInfo songInfo)
        {
            return _songInfoRepository.AddSongInfo(songInfo);
        }
    }
}
