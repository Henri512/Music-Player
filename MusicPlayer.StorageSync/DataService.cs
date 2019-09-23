using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using System.Linq;
using AutoMapper;
using System;

namespace MusicPlayer.StorageSync
{
    public class DataService
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions _dbContextOptions;
        private readonly SongInfoRepository _songInfoRepository;
        private readonly AlbumRepository _albumRepository;

        public DataService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseSqlServer(configuration.GetConnectionString("MusicPlayerCN"))
                .Options;
            var musicPlayerContext = new MusicPlayerContext(_dbContextOptions);

            _songInfoRepository = new SongInfoRepository(musicPlayerContext);
            _albumRepository = new AlbumRepository(musicPlayerContext);
        }

        public void UpdateSongInfo(SongInfo songInfo)
        {
            var existingSongInfo = _songInfoRepository
                .GetSongInfoByNameAndPath(songInfo.Name, songInfo.RelativePath)
                .SingleOrDefault();
            if(existingSongInfo != null)
            {
                _mapper.Map(songInfo, existingSongInfo);
                _songInfoRepository.UpdateSongInfo(existingSongInfo);
            }
            else
            {
                _songInfoRepository.AddSongInfo(songInfo);
            }

            _songInfoRepository.Save();
        }

        public SongInfo GetSong(string name, string relativePath)
        {
            SongInfo songInfo = null;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(relativePath))
            {
                songInfo = _songInfoRepository
                .GetSongInfoByNameAndPath(name, relativePath)
                .Include(t => t.Album)
                .SingleOrDefault();
            }
            return songInfo;
        }

        public Album GetAlbum(string albumName)
        {
            return _albumRepository
                .GetAlbum(albumName)
                .SingleOrDefault();
        }
    }
}