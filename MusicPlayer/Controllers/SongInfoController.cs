using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.Infrastructure.SongInfos;
using MusicPlayer.Utilities;

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
        public ActionResult<IEnumerable<SongInfoDto>> GetSongInfos(
                                                    bool includeAlbum = false)
        {
            return new ActionResult<IEnumerable<SongInfoDto>>(
                _songInfoService.GetSongInfos(includeAlbum));
        }

        [HttpGet]
        public ActionResult<SongInfoDto> GetSongInfoById(int id,
                                                    bool includeAlbum = false)
        {
            return new ActionResult<SongInfoDto>(
                _songInfoService.GetSongInfoById(id, includeAlbum));
        }

        [HttpPost]
        public ActionResult<SongInfoDto> AddSongInfo(SongInfoDto songInfo)
        {
            return new ActionResult<SongInfoDto>(
                _songInfoService.AddSongInfo(songInfo));
        }

        [HttpPatch]
        public ActionResult<bool> SongPlayed([FromBody]SongInfoDto songInfo)
        {
            return new ActionResult<bool>(
                _songInfoService.SongPlayed(songInfo.Id));
        }

        [HttpGet]
        public ActionResult<string> GetSongFileUrl(string songFullPath)
        {
            BlobStorageInfo blobStorageInfo = BlobStorageInfo.CreateFromEnvironmentVariable("Blobs__MusicPlayer");

            return new ActionResult<string>(
                Path.Combine(blobStorageInfo.Url, songFullPath));
        }
    }
}