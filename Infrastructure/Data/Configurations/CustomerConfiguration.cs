using Core.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure (EntityTypeBuilder<Customer> builder)
    {
        builder.HasOne<ApplicationUser>()
            .WithOne(user => user.Customer)
            .HasForeignKey<Customer>(customer => customer.IdentityId);
    }
}
