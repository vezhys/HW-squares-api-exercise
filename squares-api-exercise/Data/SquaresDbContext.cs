using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Models;

namespace squares_api_excercise.Data
{
    public class SquaresDbContext : DbContext
    {
        public DbSet<Point> Points { get; set; }

        public DbSet<Square> Squares { get; set; }

        public SquaresDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Square>()
             .HasOne(s => s.P1)
             .WithMany()
             .HasForeignKey(s => s.P1Id)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Square>()
             .HasOne(s => s.P2)
             .WithMany()
             .HasForeignKey(s => s.P2Id)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Square>()
                .HasOne(s => s.P3)
                .WithMany()
                .HasForeignKey(s => s.P3Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Square>()
                .HasOne(s => s.P4)
                .WithMany()
                .HasForeignKey(s => s.P4Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
