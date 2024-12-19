using AutoMapper;
using biblioteca.DTOs;
using biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Context;

namespace biblioteca.Services
{
    public class LibroServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper Mapper;

        public LibroServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
        }
        public async Task<List<LibroDTO>> GetULibros()
        {
            var result = _context.Libros.Include(l => l.AutorNavigation);            
            var listLibrosDTO = result.Select(x => Mapper.Map<LibroDTO>(x));
            return listLibrosDTO.ToList();
        }
    }
}
