using System.Collections.Generic;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Infrastructure.Albums
{
    public interface IAlbumService
    {
        AlbumDto AddAlbum(AlbumDto album);
        AlbumDto GetAlbumById(int id, bool includeSongInfos);
        IEnumerable<AlbumDto> GetAlbums(bool includeSongInfos);
        IEnumerable<AlbumDto> GetAlbumByFilter(
            string propertyName, string comparison, string value);
    }
}