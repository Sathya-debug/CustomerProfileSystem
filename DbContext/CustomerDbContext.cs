using CustomerProfileApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerProfileApp.DataAccess
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) :
            base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new CustomerMap(builder.Entity<Customer>());    
        }
    }
}
