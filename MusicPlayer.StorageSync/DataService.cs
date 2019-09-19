using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data;
using MusicPlayer.Data.Repositories;

namespace MusicPlayer.StorageSync
{
    public class DataService
    {
        private readonly DbContextOptions _dbContextOptions;

        public DataService(IConfiguration configuration)
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseSqlServer(configuration.GetConnectionString("MusicPlayerCN"))
                .Options;
        }

        public ISongInfoRepository GetSongInfoRepository()
        {
            var musicPlayerContext = new MusicPlayerContext(_dbContextOptions);

            return new SongInfoRepository(musicPlayerContext);
        }

        public IAlbumRepository GetAlbumRepository()
        {
            var musicPlayerContext = new MusicPlayerContext(_dbContextOptions);

            return new AlbumRepository(musicPlayerContext);
        }
    }
}