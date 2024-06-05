using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Repository : IRepository
{
    private readonly BookDbContext _db = new();
    public async Task CreateAsync(BookModel bookModel)
    {
        await _db.BookModels.AddAsync(bookModel);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        // Находим все книги, связанные с удаляемой категорией
        var booksWithCategory = await _db.BookModels.Where(x => x.CategoryId == id).ToListAsync();

        foreach (var book in booksWithCategory)
        {
            // Удаляем или обновляем связь с категорией
            book.CategoryId = 0; // Или устанавливаем null в зависимости от вашей логики
            
        }

        // Сохраняем изменения в связанных книгах
        await _db.SaveChangesAsync();

        // Теперь можно удалить саму категорию
        var category = await _db.Categories.FindAsync(id);
        
        if (category != null)
        {
            _db.Categories.Remove(category);
            var booksToDelete = await _db.BookModels.Where(x => x.CategoryId == id).ToListAsync();
            _db.BookModels.RemoveRange(booksToDelete);
            await _db.SaveChangesAsync();
        }
    }



    public Task<List<Author>> GetAllAuthorsAsync()
    => _db.Authors.ToListAsync();

    public Task<List<Category>> GetAllCategoriesAsync()
    => _db.Categories.ToListAsync();

    public async Task<BookModel> GetAsync(int id)
        => await 
            _db.BookModels.Include(b => b.Category)
            .Include(b => b.Authors).FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new Exception();

    public async Task UpdateAsync(BookModel bookModel, int id)
    {
        BookModel book = await _db.BookModels.SingleAsync(x => x.Id.Equals(id));
        book.Name = bookModel.Name;
        book.Price = bookModel.Price;
        book.FilePath = bookModel.FilePath;
        book.Category = bookModel.Category;
        book.Authors = bookModel.Authors;
        await _db.SaveChangesAsync();
    }

    public async Task<Author?> GetAuthorByNameAsync(string name)
    {
        try
        {
            return await _db.Authors
                        .Where(a => a.Name == name)
                        .FirstOrDefaultAsync();
            
        }
        catch (Exception)
        {
            _db.Authors.Add(new Author { Name = name });
            return await _db.Authors.SingleOrDefaultAsync(a => a.Name == name);
            throw;
        }   
    }

    public async Task CreateAuthorAsync(Author author)
    {
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
    }

    public async Task CreateBookAuthorAsync(BookAuthor bookAuthor)
    {
        _db.BookAuthors.Add(bookAuthor);
        await _db.SaveChangesAsync();
    }
}