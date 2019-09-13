using MusicPlayer.Models;
using System.Linq;

namespace MusicPlayer.Repositories
{
    public class SongInfoRepository : ISongInfoRepository
    {
        private readonly MusicPlayerContext _musicPlayerContext;

        public SongInfoRepository(MusicPlayerContext musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
        }

        public IQueryable<SongInfo> GetSongInfos()
        {
            return _musicPlayerContext.SongInfos;
        }

        public IQueryable<SongInfo> GetSongInfoById(int id)
        {
            return _musicPlayerContext.SongInfos.Where(s => s.Id == id);
        }

        public SongInfo AddSongInfo(SongInfo songInfo)
        {
            return _musicPlayerContext.SongInfos.Add(songInfo).Entity;
        }
    }
}
