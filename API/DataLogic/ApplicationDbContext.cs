namespace API.DataLogic
{
    using API.DataLogic.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Beacon> Beacons { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<ScheduledItem> ScheduledItems { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        
        public DbSet<Metadata> Metadata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./nearbycontent.db");
        }
    }
}