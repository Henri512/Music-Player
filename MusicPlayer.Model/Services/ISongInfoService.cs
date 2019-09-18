using System.Collections.Generic;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Model.Services
{
    public interface ISongInfoService
    {
        SongInfoModel AddSongInfo(SongInfoModel songInfo);
        IEnumerable<SongInfoModel> GetSongInfos(bool includeAlbum);
        SongInfoModel GetSongInfoById(int id, bool includeAlbum);
        bool SongPlayed(int id);
    }
}