using System.Collections.Generic;
using MusicPlayer.Models;

namespace MusicPlayer.Services
{
    public interface IAlbumService
    {
        Album AddAlbum(Album album);
        Album GetAlbumById(int id, bool includeSongInfos);
        IEnumerable<Album> GetAlbums(bool includeSongInfos);
    }
}