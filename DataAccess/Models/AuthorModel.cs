
namespace DataAccess;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<BookAuthor> BookAuthors { get; set; } = null!;
}