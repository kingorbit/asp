using Microsoft.EntityFrameworkCore;

namespace ASPprojekt.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssignmentOrder> AssignmentOrders { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
