using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager.Models;

namespace TaskManager.AppDataContext
{
    public class TaskManagerDbContext : DbContext
    {
        private readonly DbSettings _dbsettings;

        public TaskManagerDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbsettings = dbSettings.Value;
        }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.TaskType> TaskTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_dbsettings.ConnectionString);
            optionsBuilder.UseNpgsql(_dbsettings.ConnectionString);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Models.Task>()
        //    //    .ToTable("Tasks")
        //    //    .HasKey(x => x.Id);

        //    //modelBuilder.Entity<Models.TaskType>()
        //    //    .ToTable("TaskTypes")
        //    //    .HasKey(x => x.Id);
        //}
    }
}
