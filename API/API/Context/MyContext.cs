using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Employee>().HasOne(a => a.Account).WithOne(em => em.Employee).HasForeignKey<Account>(a => a.NIK);
            modelBuilder.Entity<Account>().HasOne(p => p.Profiling).WithOne(a => a.Account).HasForeignKey<Profiling>(p => p.NIK);
            modelBuilder.Entity<Education>().HasMany(e => e.Profilings).WithOne(e => e.Education);
            modelBuilder.Entity<University>().HasMany(e => e.Educations).WithOne(u => u.University);
            modelBuilder.Entity<Account>().HasMany(ar => ar.AccountRoles).WithOne(a => a.Account).HasForeignKey(p => p.NIK);
            modelBuilder.Entity<Role>().HasMany(ar => ar.AccountRoles).WithOne(r => r.Role);
        }
    }
}
