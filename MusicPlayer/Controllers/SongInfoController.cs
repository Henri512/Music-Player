using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Model.Models;
using MusicPlayer.Model.Services;

namespace MusicPlayer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    public class SongInfoController : ControllerBase
    {
        private readonly ISongInfoService _songInfoService;
        private readonly IConfiguration _configuration;

        public SongInfoController(ISongInfoService songInfoService, IConfiguration configuration)
        {
            _songInfoService = songInfoService;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongInfoModel>> GetSongInfos(
                                                    bool includeAlbum = false)
        {
            return new ActionResult<IEnumerable<SongInfoModel>>(
                _songInfoService.GetSongInfos(includeAlbum));
        }

        [HttpGet]
        public ActionResult<SongInfoModel> GetSongInfoById(int id,
                                                    bool includeAlbum = false)
        {
            return new ActionResult<SongInfoModel>(
                _songInfoService.GetSongInfoById(id, includeAlbum));
        }

        [HttpPost]
        public ActionResult<SongInfoModel> AddSongInfo(SongInfoModel songInfo)
        {
            return new ActionResult<SongInfoModel>(
                _songInfoService.AddSongInfo(songInfo));
        }

        [HttpPatch]
        public ActionResult<bool> SongPlayed([FromBody]SongInfoModel songInfo)
        {
            return new ActionResult<bool>(
                _songInfoService.SongPlayed(songInfo.Id));
        }

        [HttpGet]
        public ActionResult<string> GetSongFileUrl(string songFullPath)
        {
            var blobStoragePath = _configuration
                .GetSection("BlobStorages").GetValue<string>("SongsBlobUrl");

            return new ActionResult<string>(
                Path.Combine(blobStoragePath, songFullPath));
        }
    }
}