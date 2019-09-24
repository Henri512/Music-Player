using System.Linq;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
{
    public interface ISongInfoRepository
    {
        IQueryable<SongInfo> GetSongInfoByNameAndPath(string songName, string songPath);       
    }
}