using Microsoft.EntityFrameworkCore;
using MusicPlayer.Model.Entities;
using MusicPlayer.Model.Repositories;
using System.Linq;

namespace MusicPlayer.Repositories
{
    public class SongInfoRepository : ISongInfoRepository
    {
        private readonly DbContext _musicPlayerContext;

        private readonly DbSet<SongInfo> _songInfoDbSet;

        public SongInfoRepository(DbContext musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
            _songInfoDbSet = _musicPlayerContext.Set<SongInfo>();
        }

        public IQueryable<SongInfo> GetSongInfos()
        {
            return _songInfoDbSet;
        }

        public IQueryable<SongInfo> GetSongInfoById(int id)
        {
            return _songInfoDbSet.Where(s => s.Id == id);
        }

        public SongInfo AddSongInfo(SongInfo songInfo)
        {
            return _songInfoDbSet.Add(songInfo).Entity;
        }
    }
}
