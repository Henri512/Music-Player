using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models;
using MusicPlayer.Services;

namespace MusicPlayer.Controllers
{
    [Route("api/[controller]")]
    public class SongInfoController : Controller
    {
        private readonly ISongInfoService _songInfoService;

        public SongInfoController(ISongInfoService songInfoService)
        {
            _songInfoService = songInfoService;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<SongInfo>> GetSongInfos(bool includeAlbum = false)
        {
            return new ActionResult<IEnumerable<SongInfo>>(_songInfoService.GetSongInfos(includeAlbum));
        }

        [HttpGet("[action]")]
        public ActionResult<SongInfo> GetSongInfoById(int id, bool includeAlbum = false)
        {
            return new ActionResult<SongInfo>(_songInfoService.GetSongInfoById(id, includeAlbum));
        }

        [HttpPost("[action]")]
        public ActionResult<SongInfo> AddSongInfo(SongInfo songInfo)
        {
            return new ActionResult<SongInfo>(_songInfoService.AddSongInfo(songInfo));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}