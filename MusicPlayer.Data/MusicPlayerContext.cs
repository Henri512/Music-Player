using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Profiles;
using Album = MusicPlayer.Core.Albums.Album;
using BaseEntity = MusicPlayer.Core.BaseEntity;
using SongInfo = MusicPlayer.Core.SongInfos.SongInfo;

namespace MusicPlayer.Data
{
    public class MusicPlayerContext : DbContext
    {
        public MusicPlayerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SongInfo> SongInfos { get; set; }
        public DbSet<Album> Albums { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is BaseEntity && (
                       e.State == EntityState.Added
                       || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).LastModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlbumProfile).Assembly);
        }
    }
}
