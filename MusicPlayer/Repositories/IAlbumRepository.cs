using MusicPlayer.Models;
using System.Linq;

namespace MusicPlayer.Repositories
{
    public interface IAlbumRepository
    {
        Album AddAlbum(Album album);
        IQueryable<Album> GetAlbumById(int id);
        IQueryable<Album> GetAlbums();
    }
}