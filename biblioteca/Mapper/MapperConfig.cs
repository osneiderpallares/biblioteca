using AutoMapper;
using biblioteca.DTOs;
using biblioteca.Models;

namespace biblioteca.Mapper
{
    public class MapperConfig : Profile
    {       
        public MapperConfig()
        {
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroDTO, Libro>();
        }
    }
}
