using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly Services _services;

        public BookController()
        {
            _services = new Services();
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Загрузить изображение книги")]
        public async Task<IActionResult> CreateAsync([FromForm] FileUploadDto model,[FromForm] string Name,
            [FromForm] int Price,[FromForm] List<string> Authors,[FromForm] string Category)
        {
            
                if (model == null)
                    return BadRequest("Файл не выбран.");

                var fileName = Path.GetFileName(model.File.FileName);
                var uploadsFolder = Path.Combine("D:/mami/C#/simple-library/Photos", "uploads");
                Directory.CreateDirectory(uploadsFolder); // Создание директории, если она не существует

                var filePath = Path.Combine(uploadsFolder, fileName);
                
                using var stream = new FileStream(filePath, FileMode.Create);
                await model.File.CopyToAsync(stream); // Сохранение файла на диск
                BookDTO bookDTO = new(
                    Name, Price, filePath, Authors, Category
                );
                await _services.CreateAsync(bookDTO);
            return Ok(new { Message = "Изображение успешно загружено" });
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            BookDTO bookDTO = await _services.GetAsync(id);
            return Ok(bookDTO.FilePath);
        }
        [HttpPut("Create")]
        [SwaggerOperation(Summary = "Загрузить изображение книги")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] FileUploadDto model,[FromForm] string Name,
            [FromForm] int Price,[FromForm] List<string> Authors,[FromForm] string Category)
        {
            
                if (model == null)
                    return BadRequest("Файл не выбран.");

                var fileName = Path.GetFileName(model.File.FileName);
                var uploadsFolder = Path.Combine("D:/mami/C#/simple-library/Photos", "uploads");
                Directory.CreateDirectory(uploadsFolder); // Создание директории, если она не существует

                var filePath = Path.Combine(uploadsFolder, fileName);
                
                using var stream = new FileStream(filePath, FileMode.Create);
                await model.File.CopyToAsync(stream); // Сохранение файла на диск
                BookDTO bookDTO = new(
                    Name, Price, filePath, Authors, Category
                );
                await _services.UpdateAsync(bookDTO, id);
            return Ok(new { Message = "Изображение успешно загружено" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _services.DeleteAsync(id);
            return Ok();
        }

        public class FileUploadDto
        {
            [FromForm(Name = "file")]
            public IFormFile File { get; set; } = null!;
        }
    }
}
