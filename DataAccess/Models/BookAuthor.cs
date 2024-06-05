namespace DataAccess;

public class BookAuthor
{
    public int BookId { get; set; }
    public BookModel Book { get; set; } = null!;
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}
