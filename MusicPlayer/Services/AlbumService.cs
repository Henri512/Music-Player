using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models;
using MusicPlayer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public IEnumerable<Album> GetAlbums(bool includeSongInfos)
        {
            var albums = includeSongInfos ? _albumRepository.GetAlbums().Include(a => a.SongInfos) : _albumRepository.GetAlbums();
            return albums.ToList();
        }

        public Album GetAlbumById(int id, bool includeSongInfos)
        {
            var album = includeSongInfos ? _albumRepository.GetAlbumById(id).Include(a => a.SongInfos) : _albumRepository.GetAlbumById(id);
            return album.FirstOrDefault();
        }

        public Album AddAlbum(Album album)
        {
            return _albumRepository.AddAlbum(album);
        }
    }
}
