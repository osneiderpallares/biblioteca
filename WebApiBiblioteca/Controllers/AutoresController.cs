using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Models;
using WebApiBiblioteca.Services;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AutoresServices AutoresServices;

        public AutoresController(ApplicationDbContext context, AutoresServices AutoresServices)
        {
            _context = context;
            this.AutoresServices = AutoresServices;
        }

        // GET: api/Autores
        [HttpGet]
        [Route("listAutores")]
        public async Task<IActionResult> Index()
        {
            var result = await AutoresServices.GetAutores();

            return Ok(result);
        }

        // GET: api/Autores
        [HttpGet]
        [Route("detailsAutores")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await AutoresServices.DetailsAutores(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Libros
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(Autore autor)
        {
            if (autor == null)
            {
                return NotFound();
            }
            var result = await AutoresServices.Save(autor);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Libros
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Autore autor)
        {
            if (autor == null)
            {
                return NotFound();
            }
            var result = await AutoresServices.Update(autor);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST: api/Autores
        [HttpPost]
        [Route("deleteAutor")]
        public async Task<IActionResult> Delete(Autore autor)
        {
            if (autor == null)
            {
                return NotFound();
            }
            var result = await AutoresServices.DeleteAutores(autor);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
