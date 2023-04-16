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
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            // In Windows OS, SQLite DB file will be created on C:\Users\username\AppData\Local
            DbPath = System.IO.Path.Join(path, "Recruitment.db");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
