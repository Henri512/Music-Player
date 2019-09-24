using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Entities;
using MusicPlayer.Data.Repositories;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;

namespace MusicPlayer.Domain.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public IEnumerable<AlbumModel> GetAlbums(bool includeSongInfos)
        {
            var albums = includeSongInfos ? _albumRepository.GetAlbums()
                .Include(a => a.SongInfos) : _albumRepository.GetAlbums();
            return _mapper.Map<IEnumerable<AlbumModel>>(albums.ToList());
        }

        public AlbumModel GetAlbumById(int id, bool includeSongInfos)
        {
            var album = includeSongInfos ? 
                _albumRepository.GetAlbumById(id).Include(a => a.SongInfos)
                : _albumRepository.GetAlbumById(id);
            return _mapper.Map<AlbumModel>(album.FirstOrDefault());
        }

        public AlbumModel AddAlbum(AlbumModel albumModel)
        {
            var album = _mapper.Map<Album>(albumModel);
            var albumAdded = _albumRepository.AddAlbum(album);
            return _mapper.Map<AlbumModel>(albumAdded);
        }
    }
}
