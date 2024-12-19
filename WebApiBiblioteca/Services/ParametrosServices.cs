using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.Models;

namespace WebApiBiblioteca.Services
{
    public class ParametrosServices
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public ParametrosServices(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public async Task<Parametros> GetParametro(string? nombre)
        {
            var parametro = await Context.Parametros.FirstOrDefaultAsync(p => p.Parametro == nombre);

            if (parametro == null)
            {
                return null;
            }
            return parametro;
        }
    }
}
