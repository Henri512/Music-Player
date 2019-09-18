using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;
using FileIO = System.IO.File;

namespace MusicPlayer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    public class SongInfoController : ControllerBase
    {
        private readonly ISongInfoService _songInfoService;

        public SongInfoController(ISongInfoService songInfoService)
        {
            _songInfoService = songInfoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongInfoModel>> GetSongInfos(bool includeAlbum = false)
        {
            return new ActionResult<IEnumerable<SongInfoModel>>(_songInfoService.GetSongInfos(includeAlbum));
        }

        [HttpGet]
        public ActionResult<SongInfoModel> GetSongInfoById(int id, bool includeAlbum = false)
        {
            return new ActionResult<SongInfoModel>(_songInfoService.GetSongInfoById(id, includeAlbum));
        }

        [HttpPost]
        public ActionResult<SongInfoModel> AddSongInfo(SongInfoModel songInfo)
        {
            return new ActionResult<SongInfoModel>(_songInfoService.AddSongInfo(songInfo));
        }

        [HttpPatch]
        public ActionResult<bool> SongPlayed([FromBody]SongInfoModel songInfo)
        {
            return new ActionResult<bool>(_songInfoService.SongPlayed(songInfo.Id));
        }

        [HttpGet]
        public async Task<ActionResult<byte[]>> GetSongFile(int id)
        {
            var songInfo = _songInfoService.GetSongInfoById(id, false);
            var fileReader = FileIO.OpenRead(songInfo.FullPath);
            var songData = new byte[fileReader.Length];
            await fileReader.ReadAsync(songData);

            return new ActionResult<byte[]>(songData);
        }

        [HttpGet]
        public ActionResult<bool> CacheSongFile(int id, string cacheFolder)
        {
            var result = false;
            var songInfo = _songInfoService.GetSongInfoById(id, false);
            if (songInfo == null)
                return NotFound("Song does not exist!");
            var cachedSongPath = Path.Combine(cacheFolder, songInfo.FullName);
            if (!FileIO.Exists(cachedSongPath))
            {
                var fileReader = FileIO.OpenRead(songInfo.FullPath);
                var songData = new byte[fileReader.Length];
                fileReader.Read(songData);

                FileIO.WriteAllBytes(Path.Combine(@"ClientApp/src/" + cachedSongPath.TrimStart('.')), songData);
                result = true;
            }
            return new ActionResult<bool>(result);
        }
    }
}