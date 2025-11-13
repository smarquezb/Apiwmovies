using Apiwmovies.DAL.Models;
using Apiwmovies.DAL.Models.Dtos;
using AutoMapper;
using System.Runtime;

namespace Apiwmovies.MoviesMapper
{
    public class Mappers: Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
        }

    }
}
