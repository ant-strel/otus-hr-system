using Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework;

/// <summary>
/// Добавление миграции из VS
///
/// 1) Открыть Package Manager Console
/// 2) Выбрать Default project: Infrastructure\Infrastructure.EntityFramework
/// 3) Add-Migration НазваниеМиграции
/// 4) После успешного создания миграции в папке Infrastructure.EntityFramework\Migrations\ появятся файлы миграции
/// 5) Проверяем миграции после запуска сервера
/// </summary>

public class DatabaseContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<JobReply> Replies { get; set; }

    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    public DatabaseContext()
    {
    }
    
    public DatabaseContext(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(_loggerFactory)
            .UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobReply>()
                   .HasOne(jr => jr.Job)
                   .WithMany(j => j.JobReplies)
                   .HasForeignKey(jr => jr.JobId);

        modelBuilder.Entity<JobReply>()
            .HasOne(jr => jr.Candidate)
            .WithMany(c => c.JobReplies)
            .HasForeignKey(jr => jr.CandidateId);

        modelBuilder.Entity<JobReply>()
            .HasOne(jr => jr.Status)
            .WithOne(s => s.JobReply)
            .HasForeignKey<Status>(s => s.JobReplyId);

    }
}