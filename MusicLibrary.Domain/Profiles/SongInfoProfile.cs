using AutoMapper;
using MusicPlayer.Data.Entities;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Domain.Profiles
{
    public class SongInfoProfile : Profile
    {
        public SongInfoProfile()
        {
            this.CreateMap<SongInfo, SongInfoModel>();
        }
    }
}
