using System;
using Microsoft.EntityFrameworkCore;
using Album = MusicPlayer.Data.Entities.Album;
using SongInfo = MusicPlayer.Data.Entities.SongInfo;

namespace MusicPlayer.Data
{
    public class DbContextSeeder : IDisposable
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongInfo>()
                .HasOne(s => s.Album)
                .WithMany(a => a.SongInfos)
                .OnDelete(DeleteBehavior.SetNull);

            var songId = 1;
            var albumId = 1;

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = albumId++,
                    Name = "This Side Of The Big River",
                    Year = new DateTime(1975, 1, 1),
                    ImagePath = "../assets/album-logo-1.jpg"
                });

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = albumId++,
                    Name = "Essentials II",
                    Year = new DateTime(2014, 1, 1),
                    ImagePath = "../assets/album-logo-2.jpeg"
                });

            modelBuilder.Entity<SongInfo>().HasData(
                new SongInfo
                {
                    Id = songId++,
                    Name = "01 - Same Ol' Story ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 17),
                    Genre = "Country",
                    BitRate = "798kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]",
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "02 - Holding Me Together ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 2, 47),
                    Genre = "Country",
                    BitRate = "694kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "03 - Gettin' Older, Lookin' Back ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 24),
                    Genre = "Country",
                    BitRate = "845kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "04 - John Tucker's On The Wagon Again ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 5, 30),
                    Genre = "Country",
                    BitRate = "751kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "05 - Big River ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 3, 17),
                    Genre = "Country",
                    BitRate = "901kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "06 - May God Be With Me ",
                    AlbumId = 1,
                    Author = "Chip Taylor",
                    Duration = new TimeSpan(0, 4, 08),
                    Genre = "Country",
                    BitRate = "725kbps",
                    Extension = "FLAC",
                    RelativePath = @"Chip Taylor...This Side Of The Big River(1975)[FLAC]"
                }
            );

            albumId++;

            modelBuilder.Entity<SongInfo>().HasData(
                new SongInfo
                {
                    Id = songId++,
                    Name = "01 - The Way It Is",
                    AlbumId = 2,
                    Author = "Karen Souza",
                    Duration = new TimeSpan(0, 3, 27),
                    Genre = "Vocal Jazz",
                    BitRate = "320kbps",
                    Extension = "mp3",
                    RelativePath = @"[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)",
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "02 - Wicked Game",
                    AlbumId = 2,
                    Author = "Karen Souza",
                    Duration = new TimeSpan(0, 4, 6),
                    Genre = "Vocal Jazz",
                    BitRate = "320kbps",
                    Extension = "mp3",
                    RelativePath = @"[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)",
                },
                new SongInfo
                {
                    Id = songId++,
                    Name = "03 - Everyday Is Like Sunday",
                    AlbumId = 2,
                    Author = "Karen Souza",
                    Duration = new TimeSpan(0, 3, 42),
                    Genre = "Vocal Jazz",
                    BitRate = "320kbps",
                    Extension = "mp3",
                    RelativePath = @"[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)",
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
