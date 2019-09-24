using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbContext _musicPlayerContext;

        private readonly DbSet<Album> _albumDbSet;

        public AlbumRepository(DbContext musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
            _albumDbSet = _musicPlayerContext.Set<Album>();
        }

        public IQueryable<Album> GetAlbums()
        {
            return _albumDbSet;
        }

        public IQueryable<Album> GetAlbumById(int id)
        {
            return _albumDbSet.Where(a => a.Id == id);
        }

        public Album AddAlbum(Album album)
        {
            return _albumDbSet.Add(album).Entity;
        }

        public void UpdateAlbum(Album album)
        {
            _albumDbSet.Update(album);
        }

        public int Save()
        {
            return _musicPlayerContext.SaveChanges();
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
