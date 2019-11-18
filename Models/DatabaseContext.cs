using Microsoft.EntityFrameworkCore;

namespace ExampleGrid.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<CustomerTB> CustomerTB { get; set; }
    }
}