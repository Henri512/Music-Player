using MusicPlayer.Model.Entities;
using System.Linq;

namespace MusicPlayer.Model.Repositories
{
    public interface ISongInfoRepository
    {
        SongInfo AddSongInfo(SongInfo songInfo);
        IQueryable<SongInfo> GetSongInfoById(int id);
        IQueryable<SongInfo> GetSongInfos();
    }
}