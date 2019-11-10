using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.Core.SongInfos;

namespace MusicPlayer.Data.Profiles
{
    public class SongInfoProfile : IEntityTypeConfiguration<SongInfo>
    {
        public void Configure(EntityTypeBuilder<SongInfo> builder)
        {
            builder.ToTable("SongInfos");
            builder.Property(p => p.Author)
                .HasMaxLength(300);
            builder.Property(p => p.BitRate)
                .HasMaxLength(20);
            builder.Property(p => p.Extension)
                .HasMaxLength(5);
            builder.Property(p => p.Genre)
                .HasMaxLength(100);
            builder.Property(p => p.Name)
                .HasMaxLength(300);

            builder.HasOne(songInfo => songInfo.Album)
                .WithMany(album => album.SongInfos)
                .HasForeignKey(songInfo => songInfo.AlbumId);
        }
    }
}
