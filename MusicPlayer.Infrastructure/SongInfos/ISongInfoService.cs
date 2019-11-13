using System.Collections.Generic;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public interface ISongInfoService
    {
        SongInfo AddSongInfo(SongInfoDto songInfo);
        IEnumerable<SongInfo> GetSongInfos(bool includeAlbum);
        SongInfo GetSongInfoById(int id, bool includeAlbum);
        bool SongPlayed(int id);
    }
}