using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;

namespace MusicPlayer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<AlbumModel>> GetAlbums(bool includeSongInfos = false)
        {
            return new ActionResult<IEnumerable<AlbumModel>>(_albumService.GetAlbums(includeSongInfos));
        }

        [HttpGet("[action]")]
        public ActionResult<AlbumModel> GetAlbumById(int id, bool includeSongInfos = false)
        {
            return new ActionResult<AlbumModel>(_albumService.GetAlbumById(id, includeSongInfos));
        }

        [HttpPost("[action]")]
        public ActionResult<AlbumModel> AddAlbum(AlbumModel album)
        {
            return new ActionResult<AlbumModel>(_albumService.AddAlbum(album));
        }
    }
}