using app_authors.Dtos.Author;
using app_authors.Entities;
using AutoMapper;

namespace app_authors.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorRequestDto, Author>();
        }
    }
}