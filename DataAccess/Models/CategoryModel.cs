
namespace DataAccess;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<BookModel> Books { get; set; } = null!;
}