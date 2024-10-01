using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.Property(c => c.Id).UseIdentityByDefaultColumn();
        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        // Indexes used for the contact search query
        builder.HasIndex(c => c.FirstName);
        builder.HasIndex(c => c.LastName);
        builder.HasIndex(c => c.PhoneNumber).IsUnique();
        builder.HasIndex(c => c.Email).IsUnique();
    }
}