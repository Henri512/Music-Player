using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Core.Albums;
using MusicPlayer.Utilities.Helpers;

namespace MusicPlayer.Infrastructure.Albums
{
    public class AlbumService : GlobalService, IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IExpressionHelper _expressionHelper;
        private readonly IGenericRepository<Album> _albumGenericRepository;

        public AlbumService(IAlbumRepository albumRepository,
            IConfiguration configuration,
            IExpressionHelper expressionHelper)
            : base(configuration)
        {
            _albumRepository = albumRepository;
            _expressionHelper = expressionHelper;
            _albumGenericRepository = albumRepository as IGenericRepository<Album>;
        }

        public IEnumerable<AlbumDto> GetAlbums(bool includeSongInfos)
        {
            var albums = includeSongInfos ? _albumGenericRepository.Get()
                .Include(a => a.SongInfos) : _albumGenericRepository.Get();
            var albumModels =  albums.Select(t => t.ToDto()).ToList();

            albumModels.ForEach(m =>
            {
                m.ImagePaths =
                    m.ImagePaths != null
                    && m.ImagePaths.Any() ?
                         GetAlbumImagesUrls(m.ImagePaths)
                        : new string[] { DefaultAlbumLogoImageUrl };
            });

            return albumModels;
        }

        public IEnumerable<AlbumDto>
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

            return albums.Select(t => t.ToDto()).ToList();
        }

        public AlbumDto GetAlbumById(int id, bool includeSongInfos)
        {
            var album = includeSongInfos ?
                _albumGenericRepository.GetById(id).
                    Include(a => a.SongInfos)
                : _albumGenericRepository.GetById(id);
            var albumModel = album.FirstOrDefault()?.ToDto();

            albumModel.ImagePaths =
                albumModel.ImagePaths != null
                && albumModel.ImagePaths.Any() ?
                     GetAlbumImagesUrls(albumModel.ImagePaths)
                    : new[] { DefaultAlbumLogoImageUrl };
            return albumModel;
        }

        public AlbumDto AddAlbum(AlbumDto albumModel)
        {
            var album = Album.ToEntity(albumModel);
            var albumAdded = _albumGenericRepository.Add(album);
            return albumAdded.ToDto();
        }
    }
}
