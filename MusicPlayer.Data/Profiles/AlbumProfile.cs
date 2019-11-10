using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.Core.Albums;

namespace MusicPlayer.Data.Profiles
{
    public class AlbumProfile : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");
            builder.Property(p => p.Author)
                .HasMaxLength(200);
            builder.Property(p => p.ImagePath)
                .HasMaxLength(1000);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
