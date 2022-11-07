using Microsoft.EntityFrameworkCore;
using practicaparcial1.Models;

namespace practicaparcial1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<calculation> calc { get; set; }
    }
}
