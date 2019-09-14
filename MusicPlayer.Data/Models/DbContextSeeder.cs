using Microsoft.EntityFrameworkCore;
using MusicPlayer.Model.Entities;
using System;

namespace MusicPlayer.Data.Models
{
    public class DbContextSeeder : IDisposable
    {
        public void Seed(ModelBuilder modelBuilder)
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
                    Duration = new TimeSpan(0, 4, 08),
                    Genre = "Country",
                    BitRate = 725,
                    Extension = "FLAC",
                    PhysicalPath = @"D:\Muzika\Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                }
            );
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DbContextSeeder()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
