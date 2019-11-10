using System.Linq;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Infrastructure.SongInfos
{
    public interface ISongInfoRepository
    {
        IQueryable<SongInfo> GetSongInfoByNameAndPath(string songName, string songPath);       
    }
}