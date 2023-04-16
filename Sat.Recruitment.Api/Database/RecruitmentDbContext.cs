using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models;
using System;

namespace Sat.Recruitment.Api.Database
{
    public class RecruitmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public RecruitmentDbContext()
        {
            var path = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(path, "/Database/Recruitment.db");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
