using AutoMapper;
using DataAccess;

namespace BusinessLayer
{
    public class Services : IService
    {
        private readonly IRepository _repository;

        private readonly IMapper _mapper;
        public Services()
        {
            _repository = new Repository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Подставьте ваш профиль маппинга
            });
            _mapper = new Mapper(config);
        }
        
        public async Task CreateAsync(BookDTO bookDTO)
        {
            try
            {
                // Маппинг DTO к модели
                BookModel bookModel = _mapper.Map<BookModel>(bookDTO);

                // Сохранение книги в базу данных
                await _repository.CreateAsync(bookModel);

                // Добавление записей в таблицу связей BookAuthor
                foreach (var authorName in bookDTO.Authors)
                {
                    var author = await _repository.GetAuthorByNameAsync(authorName);
                    if (author == null)
                    {
                        // Создание нового автора, если он не существует
                        author = new Author { Name = authorName };
                        await _repository.CreateAuthorAsync(author);
                    }

                    var bookAuthor = new BookAuthor
                    {
                        BookId = bookModel.Id, // Id книги, созданной ранее
                        AuthorId = author.Id   // Id автора
                    };

                    await _repository.CreateBookAuthorAsync(bookAuthor);
                }
            }
            catch (Exception ex)
            {
                // Логирование и обработка исключений
                throw new Exception("Ошибка при создании книги", ex);
            }
        }



        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<BookDTO> GetAsync(int id)
        {
            BookModel bookModel = await _repository.GetAsync(id);
            BookDTO bookDTO = _mapper.Map<BookDTO>(bookModel);
            return bookDTO;
        }

        public async Task UpdateAsync(BookDTO bookDTO, int id)
        {
            BookModel bookModel = _mapper.Map<BookModel>(bookDTO);
            await _repository.UpdateAsync(bookModel, id);
        }
    }
}
