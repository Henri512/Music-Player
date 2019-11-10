using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicPlayer.Data
{
    class MusicPlayerContextFactory : IDesignTimeDbContextFactory<MusicPlayerContext>
    {
        public MusicPlayerContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigHelper.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<MusicPlayerContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MusicPlayerContext(optionsBuilder.Options);
        }
    }
}
