using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data;
using System.Linq;
using MusicPlayer.Core.Albums;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.Infrastructure;
using MusicPlayer.Infrastructure.Albums;
using MusicPlayer.Infrastructure.SongInfos;

namespace MusicPlayer.StorageSync
{
    public class DataService
    {
        private readonly SongInfoRepository _songInfoRepository;
        private readonly AlbumRepository _albumRepository;
        private readonly IGenericRepository<SongInfo> _songInfoGenericRepository;

        public DataService(IConfiguration configuration)
        {
            var dbContextOptions = new DbContextOptionsBuilder()
                .UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__MusicPlayerCN"))
                .Options;
            var musicPlayerContext = new MusicPlayerContext(dbContextOptions);

            _songInfoRepository = new SongInfoRepository(musicPlayerContext);
            _albumRepository = new AlbumRepository(musicPlayerContext);

            _songInfoGenericRepository = _songInfoRepository;
        }

        public void UpdateSongInfo(SongInfo songInfo)
        {
            var existingSongInfo = _songInfoRepository
                .GetSongInfoByNameAndPath(songInfo.Name, songInfo.RelativePath)
                .SingleOrDefault();
            if(existingSongInfo != null)
            {
                _songInfoGenericRepository.Update(existingSongInfo);
            }
            else
            {
                _songInfoGenericRepository.Add(songInfo);
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

        public Album GetAlbum(string albumName, string author)
        {
            return _albumRepository
                .GetAlbum(albumName, author)
                .SingleOrDefault();
        }
    }
}