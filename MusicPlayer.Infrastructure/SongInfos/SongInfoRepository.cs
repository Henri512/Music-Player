using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public class SongInfoRepository : GenericRepository<SongInfo>,
                                                ISongInfoRepository
    {
        private readonly DbContext _musicPlayerContext;

        private readonly DbSet<SongInfo> _songInfoDbSet;

        public SongInfoRepository(DbContext musicPlayerContext)
            : base(musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
            _songInfoDbSet = _musicPlayerContext.Set<SongInfo>();
        }

        public IQueryable<SongInfo> GetSongInfoByNameAndPath(
                                            string songName, 
                                            string songPath)
        {
            return _songInfoDbSet
                .Where(t => t.Name.Equals(songName,
                        System.StringComparison.InvariantCultureIgnoreCase)
                && t.RelativePath.Equals(songPath,
                        System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
