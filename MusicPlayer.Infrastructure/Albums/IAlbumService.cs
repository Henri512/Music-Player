using System.Collections.Generic;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Infrastructure.Albums
{
    public interface IAlbumService
    {
        Album AddAlbum(AlbumDto album);
        Album GetAlbumById(int id, bool includeSongInfos);
        IEnumerable<Album> GetAlbums(bool includeSongInfos);
        IEnumerable<Album> GetAlbumByFilter(
            string propertyName, string comparison, string value);
    }
}