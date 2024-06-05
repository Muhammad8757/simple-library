

namespace DataAccess;
public interface IRepository
{
    Task CreateAsync(BookModel bookModel);
    Task<BookModel> GetAsync(int id);
    Task UpdateAsync(BookModel bookModel, int id);
    Task<List<Author>> GetAllAuthorsAsync();
    Task DeleteAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Author?> GetAuthorByNameAsync(string name);
    Task CreateAuthorAsync(Author author);
    Task CreateBookAuthorAsync(BookAuthor bookAuthor);
}
