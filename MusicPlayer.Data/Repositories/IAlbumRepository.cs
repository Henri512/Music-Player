using System.Linq;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
{
    public interface IAlbumRepository
    {
        Album AddAlbum(Album album);
        IQueryable<Album> GetAlbumById(int id);
        IQueryable<Album> GetAlbums();
        int Save();
        void UpdateAlbum(Album album);
    }
}