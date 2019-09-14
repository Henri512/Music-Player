using Microsoft.EntityFrameworkCore;
using MusicPlayer.Model.Entities;
using MusicPlayer.Model.Repositories;
using System.Linq;

namespace MusicPlayer.Repositories
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
    }
}
