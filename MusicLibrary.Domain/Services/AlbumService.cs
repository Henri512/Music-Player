using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;

namespace MusicPlayer.Domain.Services
{
    public class AlbumService : GlobalService, IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository,
            IMapper mapper, IConfiguration configuration)
            : base(mapper, configuration)
        {
            _albumRepository = albumRepository;
        }

        public IEnumerable<AlbumModel> GetAlbums(bool includeSongInfos)
        {
            var albums = includeSongInfos ? _albumRepository.GetAlbums()
                .Include(a => a.SongInfos) : _albumRepository.GetAlbums();
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

        public AlbumModel GetAlbumById(int id, bool includeSongInfos)
        {
            var album = includeSongInfos ? 
                _albumRepository.GetAlbumById(id).Include(a => a.SongInfos)
                : _albumRepository.GetAlbumById(id);
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
            var albumAdded = _albumRepository.AddAlbum(album);
            return _mapper.Map<AlbumModel>(albumAdded);
        }
    }
}
