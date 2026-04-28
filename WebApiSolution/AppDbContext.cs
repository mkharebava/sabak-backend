using Domain;
using Microsoft.EntityFrameworkCore;
using WebApiSolution.Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<CardEntry> CardEntries { get; set; }
        public DbSet<PersonalInfoEntry> PersonalInfoEntries { get; set; }
    }
}
