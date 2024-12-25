using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pratik2_CodeFirstRelation.Data.Entity;

namespace Pratik2_CodeFirstRelation.Data
{
    public class PatikaSecondDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public PatikaSecondDbContext(DbContextOptions<PatikaSecondDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=asdmna;Database=PatikaCodeFirstDb2");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(e => e.Posts).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
