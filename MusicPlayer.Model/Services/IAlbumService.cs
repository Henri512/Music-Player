using System.Collections.Generic;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Model.Services
{
    public interface IAlbumService
    {
        AlbumModel AddAlbum(AlbumModel album);
        AlbumModel GetAlbumById(int id, bool includeSongInfos);
        IEnumerable<AlbumModel> GetAlbums(bool includeSongInfos);
        IEnumerable<AlbumModel> GetAlbumByFilter(
            string propertyName, string comparison, string value);
    }
}