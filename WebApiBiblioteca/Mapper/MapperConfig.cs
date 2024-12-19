using AutoMapper;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Models;

namespace WebApiBiblioteca.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroDTO, Libro>();
            CreateMap<Autore, AutoresDTO>();
            CreateMap<AutoresDTO, Autore>();
        }
    }
}
