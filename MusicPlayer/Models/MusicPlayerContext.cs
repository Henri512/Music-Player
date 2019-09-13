using Microsoft.EntityFrameworkCore;
using System;

namespace MusicPlayer.Models
{
    public class MusicPlayerContext : DbContext
    {
        public MusicPlayerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SongInfo> SongInfos { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SongInfo>()
                .HasOne(s => s.Album)
                .WithMany(a => a.SongInfos)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = 1,
                    Name = "This Side Of The Big River",
                    Year = new DateTime(1975, 1, 1)
                });

            modelBuilder.Entity<SongInfo>().HasData(
                new SongInfo
                {
                    Id = 1,
                    Name = "01 - Same Ol' Story ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 17),
                    Genre = "Country",
                    BitRate = 798,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]",
                },
                new SongInfo
                {
                    Id = 2,
                    Name = "02 - Holding Me Together ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 2, 47),
                    Genre = "Country",
                    BitRate = 694,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = 3,
                    Name = "03 - Gettin' Older, Lookin' Back ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 24),
                    Genre = "Country",
                    BitRate = 845,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = 4,
                    Name = "04 - John Tucker's On The Wagon Again ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 5, 30),
                    Genre = "Country",
                    BitRate = 751,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = 5,
                    Name = "05 - Big River ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 17),
                    Genre = "Country",
                    BitRate = 901,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = 6,
                    Name = "06 - May God Be With Me ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Genre = "Country",
                    BitRate = 725,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                }
            );
        }
    }
}
