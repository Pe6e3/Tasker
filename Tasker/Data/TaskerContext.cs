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

        public DbSet<Tasker.Models.Category> Category { get; set; } = default!;

        public DbSet<Tasker.Models.Roles> Roles { get; set; } = default!;

        public DbSet<Tasker.Models.Taskk> Task { get; set; } = default!;

        public DbSet<Tasker.Models.User> User { get; set; } = default!;

        public DbSet<Tasker.Models.Status> Status { get; set; } = default!;
    }
}
