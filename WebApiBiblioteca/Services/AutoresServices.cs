using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Models;

namespace WebApiBiblioteca.Services
{
    public class AutoresServices
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public AutoresServices(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public async Task<List<AutoresDTO>> GetAutores()
        {
            var result = await Context.Autores.ToListAsync();
            var listAutoresDTO = result.Select(x => Mapper.Map<AutoresDTO>(x));
            return listAutoresDTO.ToList();
        }
        public async Task<AutoresDTO> DetailsAutores(int? id)
        {
            var autor = await Context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);

            if (autor == null)
            {
                return null;
            }
            var AutoresDTO = Mapper.Map<AutoresDTO>(autor);
            return AutoresDTO;
        }
        public async Task<bool> Save(Autore autor)
        {
            Context.Add(autor);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Autore autor)
        {
            Context.Update(autor);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAutores(Autore autor)
        {
            if (autor == null)
            {
                return false;
            }
            Context.Autores.Remove(autor);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
