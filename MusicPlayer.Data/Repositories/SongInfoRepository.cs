using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
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

        public IQueryable<SongInfo> GetSongInfoByNameAndPath(string songName, string songPath)
        {
            return _songInfoDbSet
                .Where(t => t.Name.Equals(songName, System.StringComparison.InvariantCultureIgnoreCase)
                && t.RelativePath.Equals(songPath, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public SongInfo AddSongInfo(SongInfo songInfo)
        {
            return _songInfoDbSet.Add(songInfo).Entity;
        }

        public void UpdateSongInfo(SongInfo songInfo)
        {
            _songInfoDbSet.Update(songInfo);
        }

        public int Save()
        {
            return _musicPlayerContext.SaveChanges();
        }
    }
}
