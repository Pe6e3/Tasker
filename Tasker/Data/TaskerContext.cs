using Microsoft.EntityFrameworkCore;
using Tasker.Models;

namespace Tasker.Data
{
    public class TaskerContext : DbContext
    {
        public TaskerContext (DbContextOptions<TaskerContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;

        public DbSet<Mission> Missions { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Status> Statuses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>()
                .HasOne(t => t.UserDoer)
                .WithMany(u => u.DoerMissions)
                .HasForeignKey(t => t.DoerUserId);

            modelBuilder.Entity<Mission>()
                .HasOne(t => t.UserMaster)
                .WithMany(u => u.MasterMissions)
                .HasForeignKey(t => t.MissionMasterUserId);
        }



    }
}
