using System.Collections.Generic;
using MusicPlayer.Models;

namespace MusicPlayer.Services
{
    public interface ISongInfoService
    {
        SongInfo AddSongInfo(SongInfo songInfo);
        IEnumerable<SongInfo> GetSongInfos(bool includeAlbum);
        SongInfo GetSongInfoById(int id, bool includeAlbum);
    }
}