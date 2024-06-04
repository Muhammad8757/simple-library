using AutoMapper;
using DataAccess;

namespace BusinessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDTO, BookModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Игнорируем Id, так как его нет в DTO
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // Игнорируем CategoryId
                .ForMember(dest => dest.BookAuthors, opt => opt.Ignore()) // Игнорируем BookAuthors
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(name => new Author { Name = name }).ToList())) // Маппинг Authors
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Name = src.Category })); // Маппинг Category
            CreateMap<BookModel, BookDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(author => author.Name).ToList()));
        }
    }
}
