using MusicPlayer.Models;
using System.Linq;

namespace MusicPlayer.Repositories
{
    public interface ISongInfoRepository
    {
        SongInfo AddSongInfo(SongInfo songInfo);
        IQueryable<SongInfo> GetSongInfoById(int id);
        IQueryable<SongInfo> GetSongInfos();
    }
}