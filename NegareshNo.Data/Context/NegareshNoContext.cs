using Microsoft.EntityFrameworkCore;
using NegareshNo.Data.Mapping;
using NegareshNo.Data.Model.Access;
using NegareshNo.Data.Model.Consulting;
using NegareshNo.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Context
{
    public class NegareshNoContext : DbContext
    {
        public NegareshNoContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Role_PermmisionMap());
            modelBuilder.ApplyConfiguration(new Role_ConsultantMap());
            modelBuilder.ApplyConfiguration(new Consultant_GroupMap());

            modelBuilder.Entity<Consultant>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<ConsultingGroup>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<UserRequest>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
        }

        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ConsultingGroup> ConsultingGroups { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permmision> Permmisions { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Role_Permmision> Role_Permmisions { get; set; }
        public DbSet<Role_Consultant> Role_Consultants { get; set; }
        public DbSet<Consultant_Group> Consultant_Groups { get; set; }

        //ToDo Migration Part
    }
}
