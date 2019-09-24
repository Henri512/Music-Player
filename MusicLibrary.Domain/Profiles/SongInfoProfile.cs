using AutoMapper;
using MusicPlayer.Data.Entities;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Domain.Profiles
{
    public class SongInfoProfile : Profile
    {
        public SongInfoProfile()
        {
            this.CreateMap<SongInfo, SongInfoModel>()
                .ForMember(m => m.AlbumImagePath, opt =>
                opt.MapFrom(s => string.IsNullOrEmpty(s.Album.ImagePath) ? 
                null : s.Album.ImagePath.Split(';',
                                    System.StringSplitOptions.None)));
        }
    }
}
