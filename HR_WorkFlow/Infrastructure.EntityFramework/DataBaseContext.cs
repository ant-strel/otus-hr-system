using Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<JobPostingStatus> JobPostingStatuses { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Command>()
                .HasOne(c => c.StartStatus)
                .WithMany(s => s.Commands)
                .HasForeignKey(c => c.StartStatusId);

            modelBuilder.Entity<JobPostingStatus>()
                .HasOne(jps => jps.JobPosting)
                .WithMany()
                .HasForeignKey(jps => jps.JobPostingId);

            modelBuilder.Entity<JobPostingStatus>()
                .HasOne(jps => jps.Status)
                .WithMany(s => s.JobPostingStatuses)
                .HasForeignKey(jps => jps.StatusId);

            modelBuilder.Entity<Resolution>()
                .HasOne(r => r.JobPosting)
                .WithMany()
                .HasForeignKey(r => r.JobPostingId);

            modelBuilder.Entity<Resolution>()
                .HasOne(r => r.Status)
                .WithMany(s => s.Resolutions)
                .HasForeignKey(r => r.StatusId);
        }
    }
}