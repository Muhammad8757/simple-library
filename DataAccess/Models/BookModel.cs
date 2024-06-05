using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess;

public class BookModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required string FilePath { get; set; }
    
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public List<BookAuthor> BookAuthors { get; set; } = null!;
    public List<Author> Authors { get; set; } = null!;
}