using Microsoft.EntityFrameworkCore;
using Parser.Database.Models;

namespace Parser.Database.Context
{
    public sealed class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
            Database.SetCommandTimeout(1);
        }

        public DbSet<WebsiteContent> WebsitesContent { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Parser;User Id=sa;Password=cGFzc3dvcmQK;Connection Timeout=1");
        }
    }
}