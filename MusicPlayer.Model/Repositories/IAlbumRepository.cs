using MusicPlayer.Model.Entities;
using System.Linq;

namespace MusicPlayer.Model.Repositories
{
    public interface IAlbumRepository
    {
        Album AddAlbum(Album album);
        IQueryable<Album> GetAlbumById(int id);
        IQueryable<Album> GetAlbums();
    }
}