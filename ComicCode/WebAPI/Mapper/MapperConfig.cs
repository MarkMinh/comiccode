using AutoMapper;
using WebAPI.Dto;
using WebAPI.Dto.Author;
using WebAPI.Dto.Chapter;
using WebAPI.Dto.Comic;
using WebAPI.Dto.Page;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, LoginDto>().ReverseMap();

            CreateMap<Author, AuthorAddingDto>().ReverseMap();
            CreateMap<ComicAddingDto, Comic>()
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Chapter, ChapterAddingDto>().ReverseMap();
            CreateMap<Chapter, ChapterDto>().ReverseMap();

            CreateMap<Page, PageDto>();
            CreateMap<Page, PageAddingDto>().ReverseMap();
        }
    }
}
