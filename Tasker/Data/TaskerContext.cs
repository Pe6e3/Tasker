using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<Tasker.Models.Category> Categories { get; set; } = default!;

        public DbSet<Tasker.Models.Role> Roles { get; set; } = default!;

        public DbSet<Tasker.Models.Taskk> Tasks { get; set; } = default!;

        public DbSet<Tasker.Models.User> Users { get; set; } = default!;

        public DbSet<Tasker.Models.Status> Statuses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Taskk>()
                .HasOne(t => t.UserDoer)
                .WithMany(u => u.DoerTasks)
                .HasForeignKey(t => t.DoerUserId);

            modelBuilder.Entity<Taskk>()
                .HasOne(t => t.UserMaster)
                .WithMany(u => u.MasterTasks)
                .HasForeignKey(t => t.TaskMasterUserId);
        }



    }
}
