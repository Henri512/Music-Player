using System.Linq;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Infrastructure.Albums
{
    public interface IAlbumRepository
    {
        IQueryable<Album> GetAlbum(string albumName, string author);
    }
}