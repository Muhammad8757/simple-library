using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class BookConfiguration : IEntityTypeConfiguration<BookModel>
{
    public void Configure(EntityTypeBuilder<BookModel> builder)
    {
        builder
            .ToTable("book_model");
        
        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(25)")
            .IsRequired();
        
        builder
            .Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("INTEGER")
            .IsRequired();
        
        builder
            .Property(p => p.FilePath)
            .HasColumnName("file_path")
            .HasColumnType("VARCHAR(500)")
            .IsRequired();
        
        builder
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);
        
        builder
            .Property(p => p.CategoryId)
            .HasColumnName("category_id");

        
        
        builder
            .HasKey(k => k.Id);
    }
}