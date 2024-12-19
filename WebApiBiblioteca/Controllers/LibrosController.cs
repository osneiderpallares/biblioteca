using WebApiBiblioteca.Models;
using WebApiBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.DTOs;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly LibrosServices LibrosServices;

        public LibrosController(ApplicationDbContext context, LibrosServices LibrosServices)
        {
            _context = context;
            this.LibrosServices = LibrosServices;
        }

        // GET: api/Libros
        [HttpGet]
        [Route("listLibros")]
        public async Task<IActionResult> Index()
        {
            var result = await LibrosServices.GetLibros();

            return Ok(result);
        }
        // GET: api/Libros
        [HttpGet]
        [Route("detailsLibro")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await LibrosServices.DetailsLibros(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Libros
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(Libro libro)
        {
            if (libro == null)
            {
                return NotFound();
            }
            var result = await LibrosServices.Save(libro);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Libros
        [HttpPost]
        [Route("deleteLibro")]
        public async Task<IActionResult> Delete(Libro libro)
        {
            if (libro == null)
            {
                return NotFound();
            }
            var result = await LibrosServices.DeleteLibros(libro);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Libros
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Libro libro)
        {
            if (libro == null)
            {
                return NotFound();
            }
            var result = await LibrosServices.Update(libro);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
