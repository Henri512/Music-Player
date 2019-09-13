using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models;
using MusicPlayer.Services;

namespace MusicPlayer.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Album>> GetAlbums(bool includeSongInfos = false)
        {
            return new ActionResult<IEnumerable<Album>>(_albumService.GetAlbums(includeSongInfos));
        }

        [HttpGet("[action]")]
        public ActionResult<Album> GetAlbumById(int id, bool includeSongInfos = false)
        {
            return new ActionResult<Album>(_albumService.GetAlbumById(id, includeSongInfos));
        }

        [HttpPost("[action]")]
        public ActionResult<Album> AddAlbum(Album Album)
        {
            return new ActionResult<Album>(_albumService.AddAlbum(Album));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}