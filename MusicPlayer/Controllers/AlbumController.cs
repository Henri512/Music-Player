using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Core.Albums;
using MusicPlayer.Infrastructure.Albums;

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

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>>
            GetAlbums(bool includeSongInfos = false)
        {
            return new ActionResult<IEnumerable<AlbumDto>>(
                _albumService.GetAlbums(includeSongInfos).Select(a=> a.ToDto()));
        }

        [HttpGet]
        public ActionResult<AlbumDto> 
            GetAlbumById(int id, bool includeSongInfos = false)
        {
            return new ActionResult<AlbumDto>(
                _albumService.GetAlbumById(id, includeSongInfos).ToDto());
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> 
            GetAlbumByFilter(string propertyName,
                string value,
                string comparison = "Contains")
        {
            return new ActionResult<IEnumerable<AlbumDto>>(
                _albumService
                    .GetAlbumByFilter(propertyName, comparison, value).Select(a => a.ToDto()));
        }

        [HttpPost]
        public ActionResult<AlbumDto> AddAlbum(AlbumDto album)
        {
            return new ActionResult<AlbumDto>(
                _albumService.AddAlbum(album).ToDto());
        }
    }
}