using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("category_model");
        
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
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasKey(k => k.Id);
    }
}