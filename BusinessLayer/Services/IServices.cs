namespace BusinessLayer;
public interface IService
{
    Task CreateAsync(BookDTO bookModel);
    Task<BookDTO> GetAsync(int id);
    Task UpdateAsync(BookDTO bookModel, int id);
    Task DeleteAsync(int id);
}
