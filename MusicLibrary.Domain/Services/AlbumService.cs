using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;
using MusicPlayer.Utilities.Helpers;

namespace MusicPlayer.Domain.Services
{
    public class AlbumService : GlobalService, IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IExpressionHelper _expressionHelper;
        private readonly IGenericRepository<Album> _albumGenericRepository;

        public AlbumService(IAlbumRepository albumRepository,
            IMapper mapper, IConfiguration configuration,
            IExpressionHelper expressionHelper)
            : base(mapper, configuration)
        {
            _albumRepository = albumRepository;
            _expressionHelper = expressionHelper;
            _albumGenericRepository = albumRepository as IGenericRepository<Album>;
        }

        public IEnumerable<AlbumModel> GetAlbums(bool includeSongInfos)
        {
            var albums = includeSongInfos ? _albumGenericRepository.Get()
                .Include(a => a.SongInfos) : _albumGenericRepository.Get();
            var albumModels = _mapper
                .Map<IEnumerable<AlbumModel>>(albums.ToList())
                .ToList();

            albumModels.ForEach(m =>
            {
                m.ImagePaths =
                    m.ImagePaths != null
                    && m.ImagePaths.Any() ?
                         GetAlbumImagesUrls(m.ImagePaths)
                        : new string[] { _defaultAlbumLogoImageUrl };
            });

            return albumModels;
        }

        public IEnumerable<AlbumModel>
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

            return _mapper.Map<IEnumerable<AlbumModel>>(albums);
        }

        public AlbumModel GetAlbumById(int id, bool includeSongInfos)
        {
            var album = includeSongInfos ?
                _albumGenericRepository.GetById(id).
                    Include(a => a.SongInfos)
                : _albumGenericRepository.GetById(id);
            var albumModel = _mapper.Map<AlbumModel>(album.FirstOrDefault());

            albumModel.ImagePaths =
                albumModel.ImagePaths != null
                && albumModel.ImagePaths.Any() ?
                     GetAlbumImagesUrls(albumModel.ImagePaths)
                    : new string[] { _defaultAlbumLogoImageUrl };
            return albumModel;
        }

        public AlbumModel AddAlbum(AlbumModel albumModel)
        {
            var album = _mapper.Map<Album>(albumModel);
            var albumAdded = _albumGenericRepository.Add(album);
            return _mapper.Map<AlbumModel>(albumAdded);
        }
    }
}
