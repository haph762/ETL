using ETL.Domain;
using Microsoft.EntityFrameworkCore;

namespace ETL.Infrastructure.DataSqlServer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
