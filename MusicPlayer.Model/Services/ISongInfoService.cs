using System.Collections.Generic;
using MusicPlayer.Model.Entities;

namespace MusicPlaSyer.Model.Services
{
    public interface ISongInfoService
    {
        SongInfo AddSongInfo(SongInfo songInfo);
        IEnumerable<SongInfo> GetSongInfos(bool includeAlbum);
        SongInfo GetSongInfoById(int id, bool includeAlbum);
    }
}