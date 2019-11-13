using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Albums;
using MusicPlayer.Infrastructure.Blobs;
using MusicPlayer.Utilities.Helpers;

namespace MusicPlayer.Infrastructure.Albums
{
    public class AlbumService : IAlbumService
    {
        private readonly IExpressionHelper _expressionHelper;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IGenericRepository<Album> _albumGenericRepository;

        public AlbumService(IAlbumRepository albumRepository,
            IExpressionHelper expressionHelper,
            IBlobStorageService blobStorageService)
        {
            _expressionHelper = expressionHelper;
            _blobStorageService = blobStorageService;
            _albumGenericRepository = albumRepository as IGenericRepository<Album>;
        }

        public IEnumerable<Album> GetAlbums(bool includeSongInfos)
        {
            var query = includeSongInfos ? _albumGenericRepository.Get()
                .Include(a => a.SongInfos) : _albumGenericRepository.Get();
            var albums = query.ToList();
            albums.ForEach(a => a.ImagePath = string.IsNullOrEmpty(a.ImagePath) ?
                _blobStorageService.DefaultAlbumLogoImageUrl : a.ImagePath);

            return albums;
        }

        public IEnumerable<Album>
            GetAlbumByFilter(
                string propertyName,
                string comparison,
                string value)
        {
            var predicate = _expressionHelper
                .BuildPredicate<Album>(propertyName, comparison, value);
            var albums = _albumGenericRepository
                .Get()
                .Where(predicate)
                .ToList();
            albums.ForEach(a => a.ImagePath = string.IsNullOrEmpty(a.ImagePath) ?
                _blobStorageService.DefaultAlbumLogoImageUrl : a.ImagePath);

            return albums;
        }

        public Album GetAlbumById(int id, bool includeSongInfos)
        {
            var query = includeSongInfos ?
                _albumGenericRepository.GetById(id).
                    Include(a => a.SongInfos)
                : _albumGenericRepository.GetById(id);
            var album = query.FirstOrDefault();

            album.ImagePath = string.IsNullOrEmpty(album.ImagePath)
                ? _blobStorageService.DefaultAlbumLogoImageUrl
                : album.ImagePath;

            return album;
        }

        public Album AddAlbum(AlbumDto albumModel)
        {
            var album = Album.ToEntity(albumModel);
            var albumAdded = _albumGenericRepository.Add(album);
            album.ImagePath = string.IsNullOrEmpty(album.ImagePath)
                ? _blobStorageService.DefaultAlbumLogoImageUrl
                : album.ImagePath;
            return albumAdded;
        }
    }
}
