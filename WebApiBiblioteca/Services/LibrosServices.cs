using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace WebApiBiblioteca.Services
{
    public class LibrosServices
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public LibrosServices(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public async Task<List<LibroDTO>> GetLibros()
        {
            var result = await Context.Libros.Include(l => l.AutorNavigation).ToListAsync();
            var listLibrosDTO = result.Select(x => Mapper.Map<LibroDTO>(x));
            return listLibrosDTO.ToList();
        }
        public async Task<LibroDTO> DetailsLibros(int? id)
        {            
            var libro = await Context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libro == null)
            {
                return null; 
            }
            var libroDTO = Mapper.Map<LibroDTO>(libro);
            return libroDTO;
        }
        public async Task<bool> Save(Libro libro)
        {
            Context.Add(libro);
            await Context.SaveChangesAsync();            
            return true;
        }
        public async Task<bool> DeleteLibros(Libro libro)
        {
            if (libro == null)
            {
                return false;
            }
            Context.Libros.Remove(libro);
            await Context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(Libro libro)
        {
            Context.Update(libro);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
