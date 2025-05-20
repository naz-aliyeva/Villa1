using Microsoft.EntityFrameworkCore;
using Villa1.Models;

namespace Villa1.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) 
        {
        }
        public DbSet<Best> Bests { get; set; }
    }
}
