using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GymManagementSystemDAL.Data.Contexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext>options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecord { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sessoin> sessoins { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

    }
}
