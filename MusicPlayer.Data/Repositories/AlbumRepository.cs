using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
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
                .Where(t => t.Name
                .Equals(
                    albumName, StringComparison.InvariantCultureIgnoreCase)
                && t.Author
                .Equals(author, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
