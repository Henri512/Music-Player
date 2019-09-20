using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using System.Linq;

namespace MusicPlayer.StorageSync
{
    public class DataService
    {
        private readonly DbContextOptions _dbContextOptions;
        private readonly SongInfoRepository _songInfoRepository;
        private readonly AlbumRepository _albumRepository;

        public DataService(IConfiguration configuration)
        {
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
                _songInfoRepository.UpdateSongInfo(songInfo);
            }
            else
            {
                _songInfoRepository.AddSongInfo(songInfo);
            }

            _songInfoRepository.Save();
        }
    }
}