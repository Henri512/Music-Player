using System.Linq;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
{
    public interface IAlbumRepository
    {
        IQueryable<Album> GetAlbum(string albumName, string author);
    }
}