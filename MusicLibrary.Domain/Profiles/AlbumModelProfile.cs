using AutoMapper;
using MusicPlayer.Data.Entities;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Domain.Profiles
{
    public class AlbumModelProfile : Profile
    {
        public AlbumModelProfile()
        {
            this.CreateMap<Album, AlbumModel>();
        }
    }
}
