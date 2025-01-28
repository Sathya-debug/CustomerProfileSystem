using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerProfileApp.Model
{
    public class CustomerMap
    {
        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder) {
            entityBuilder.HasKey(t => t.CustomerId);
            entityBuilder.Property(t => t.FirstName).IsRequired();
            entityBuilder.Property(t => t.LastName).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
            entityBuilder.Property(t => t.Age).IsRequired();
            entityBuilder.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(10);
            entityBuilder.Property(t => t.Location).IsRequired();
        }
    }
}
