using MusicPlayer.Models;
using System.Linq;

namespace MusicPlayer.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicPlayerContext _musicPlayerContext;

        public AlbumRepository(MusicPlayerContext musicPlayerContext)
        {
            _musicPlayerContext = musicPlayerContext;
        }

        public IQueryable<Album> GetAlbums()
        {
            return _musicPlayerContext.Albums;
        }

        public IQueryable<Album> GetAlbumById(int id)
        {
            return _musicPlayerContext.Albums.Where(a => a.Id == id);
        }

        public Album AddAlbum(Album album)
        {
            return _musicPlayerContext.Albums.Add(album).Entity;
        }
    }
}
