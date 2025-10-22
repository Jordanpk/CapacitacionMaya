using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class PlayStationData : DbContext
    {
        public PlayStationData(DbContextOptions<PlayStationData>options): base(options) 
        {
        }


        public DbSet<Games> Games => Set<Games>();
        public DbSet<Simpson> Simpson => Set<Simpson>();
        public DbSet<Movies> Movies => Set<Movies>();
        public DbSet<Beers> Beers => Set<Beers>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Games>().ToTable("Games");
            modelBuilder.Entity<Games>().HasKey(g => g.id);
            modelBuilder.Entity<Simpson>().ToTable("Simpson");
            modelBuilder.Entity<Simpson>().HasKey(s => s.id);
            modelBuilder.Entity<Simpson>().Property(s => s.id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Movies>().ToTable("Movies");
            modelBuilder.Entity<Movies>().HasKey(m => m.id);
            modelBuilder.Entity<Movies>().Property(m => m.genero).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<Beers>().ToTable("Beers");
            modelBuilder.Entity<Beers>().HasKey(b => b.id);
            modelBuilder.Entity<Beers>().Property<int>(b => b.id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Beers>().Property(b => b.id).HasColumnName("id_beer");
            modelBuilder.Entity<Beers>().Property(b => b.type_beer).HasConversion<string>().HasMaxLength(50);

        }
    }
}
