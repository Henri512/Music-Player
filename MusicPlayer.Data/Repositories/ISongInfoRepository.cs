using System.Linq;
using MusicPlayer.Data.Entities;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Data.Repositories
{
    public interface ISongInfoRepository
    {
        SongInfo AddSongInfo(SongInfo songInfo);
        IQueryable<SongInfo> GetSongInfoById(int id);
        IQueryable<SongInfo> GetSongInfos();
        int Save();
        void UpdateSongInfo(SongInfo songInfo);
    }
}