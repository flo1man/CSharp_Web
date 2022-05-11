using Microsoft.EntityFrameworkCore;
using Suls.Data.Models;

namespace SulsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Problem> Problems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Suls;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.Submissions)
                .HasForeignKey(x => x.UserId);

                x.HasOne(x => x.Problem)
                .WithMany(x => x.Submissions)
                .HasForeignKey(x => x.ProblemId);
            });
        }
    }
}