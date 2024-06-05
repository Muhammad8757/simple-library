using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class AuthorModelConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .ToTable("author_model");
        
        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

                    
        builder
            .HasKey(k => k.Id);
    }
}