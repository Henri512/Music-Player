using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Infrastructure.Albums
{
    public class AlbumRepository : GenericRepository<Album>,
                                    IAlbumRepository
    {
        private readonly DbContext _musicPlayerContext;

        private readonly DbSet<Album> _albumDbSet;

        public AlbumRepository(DbContext musicPlayerContext)
            : base(musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
            _albumDbSet = _musicPlayerContext.Set<Album>();
        }

        public IQueryable<Album> GetAlbum(string albumName, string author)
        {
            return _albumDbSet
                .Where(t => EF.Functions.Like(t.Name,albumName)
                && EF.Functions.Like(t.Author, author));
        }
    }
}
