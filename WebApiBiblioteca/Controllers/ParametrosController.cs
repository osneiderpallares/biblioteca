using Microsoft.AspNetCore.Mvc;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.Services;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ParametrosServices ParametrosServices;

        public ParametrosController(ApplicationDbContext context, ParametrosServices parametrosServices)
        {
            _context = context;
            this.ParametrosServices = parametrosServices;
        }
        // GET: api/Parametros
        [HttpGet]
        [Route("parametro")]
        public async Task<IActionResult> GetParametro(string? parametro)
        {
            if (parametro == "")
            {
                return NotFound();
            }
            var result = await ParametrosServices.GetParametro(parametro);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
