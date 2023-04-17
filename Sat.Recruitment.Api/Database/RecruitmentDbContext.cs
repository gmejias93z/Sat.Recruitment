using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models;
using System;

namespace Sat.Recruitment.Api.Database
{
    public class RecruitmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; } = System.IO.Path.Join(Environment.CurrentDirectory, "/Database/Recruitment.db");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
