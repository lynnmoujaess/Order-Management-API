using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement_Api.Models;

namespace OrderManagement_Api.Data.Configurations;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(200);
        
        builder.Property(c => c.Email).HasMaxLength(200);
        
        builder.HasIndex(c => c.Email).IsUnique();
    }
}