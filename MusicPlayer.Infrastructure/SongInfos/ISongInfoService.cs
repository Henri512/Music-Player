using System.Collections.Generic;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public interface ISongInfoService
    {
        SongInfoDto AddSongInfo(SongInfoDto songInfo);
        IEnumerable<SongInfoDto> GetSongInfos(bool includeAlbum);
        SongInfoDto GetSongInfoById(int id, bool includeAlbum);
        bool SongPlayed(int id);
    }
}