using System.Collections.Generic;
using MusicPlayer.Model.Entities;

namespace MusicPlayer.Model.Services
{
    public interface IAlbumService
    {
        Album AddAlbum(Album album);
        Album GetAlbumById(int id, bool includeSongInfos);
        IEnumerable<Album> GetAlbums(bool includeSongInfos);
    }
}