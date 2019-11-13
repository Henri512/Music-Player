using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.Infrastructure.Blobs;
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
        private readonly IBlobStorageService _blobStorageService;

        public SongInfoController(ISongInfoService songInfoService, IConfiguration configuration, IBlobStorageService blobStorageService)
        {
            _songInfoService = songInfoService;
            _configuration = configuration;
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongInfoDto>> GetSongInfos(
                                                    bool includeAlbum = false)
        {
            List<SongInfoDto> songInfoDtos = _songInfoService
                .GetSongInfos(includeAlbum)
                .Select(s => s.ToDto())
                .ToList();
            SetBlobProperties(songInfoDtos);
            return new ActionResult<IEnumerable<SongInfoDto>>(songInfoDtos);
        }

        [HttpGet]
        public ActionResult<SongInfoDto> GetSongInfoById(int id,
                                                    bool includeAlbum = false)
        {
            SongInfoDto songInfoDto = _songInfoService
                .GetSongInfoById(id, includeAlbum)
                .ToDto();
            SetBlobProperties(songInfoDto);
            return new ActionResult<SongInfoDto>(songInfoDto);
        }

        [HttpPost]
        public ActionResult<SongInfoDto> AddSongInfo(SongInfoDto songInfo)
        {
            return new ActionResult<SongInfoDto>(
                _songInfoService.AddSongInfo(songInfo).ToDto());
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

        private void SetBlobProperties(List<SongInfoDto> songInfoDtos)
        {
            songInfoDtos.ForEach(SetBlobProperties);
        }

        private void SetBlobProperties(SongInfoDto songDto)
        {
            songDto.BlobFileReference = _blobStorageService.GetBlobFileUriLocation(songDto.RelativePath);

            songDto.AlbumImagePath = songDto.AlbumImagePath != null
                                  && songDto.AlbumImagePath.Any() ?
                _blobStorageService.GetAlbumImagesUrls(songDto.AlbumImagePath)
                : new[] { _blobStorageService.DefaultAlbumLogoImageUrl };
        }
    }
}