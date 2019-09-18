using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Album = MusicPlayer.Data.Entities.Album;
using BaseEntity = MusicPlayer.Data.Entities.BaseEntity;
using SongInfo = MusicPlayer.Data.Entities.SongInfo;

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
            using (var seeder = new DbContextSeeder())
            {
                seeder.Seed(modelBuilder);
            }
        }
    }
}
