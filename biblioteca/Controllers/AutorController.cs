using biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApiBiblioteca.DTOs;

namespace biblioteca.Controllers
{
    public class AutorController : Controller
    {
        private readonly HttpClient _httpClient;

        public AutorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44315/api");
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Autores/listAutores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<AutoresDTO>>(content);
                return View("Index", result);
            }

            return View(new List<AutoresDTO>());
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AutoresDTO>(content);
                return View(result);
            }

            return View(new AutoresDTO());
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autore autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(autor), Encoding.UTF8, "application/json");
                    var respuesta = await _httpClient.PostAsync("/api/Autores/save", contenido);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Manejar el error
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        // Puedes registrar el error o mostrar un mensaje al usuario
                        return View("Error", errorContent);
                    }
                }
                
                return View(autor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage2 = ex.Message;
                var rep = await _httpClient.GetAsync("/api/Autores/listAutores");
                if (rep.IsSuccessStatusCode)
                {
                    var contentAutor = await rep.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<AutoresDTO>>(contentAutor);
                    var selectList = result.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nombre
                    }).ToList();
                    ViewData["Autor"] = selectList;
                }
                return View(new AutoresDTO());
            }
        }
        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var autor = JsonConvert.DeserializeObject<AutoresDTO>(content);

            if (autor == null)
            {
                return NotFound();
            }           
            return View(autor);
        }
        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Autore autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(autor), Encoding.UTF8, "application/json");
                    var respuesta = await _httpClient.PostAsync("/api/Autores/update", contenido);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Manejar el error
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        // Puedes registrar el error o mostrar un mensaje al usuario
                        return View("Error", errorContent);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={id}");
                    var content = await response.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<AutoresDTO>(content);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }           
            return View(autor);
        }
        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AutoresDTO>(content);
                if (result == null)
                {
                    return NotFound();
                }

                return View(result);
            }

            return View(new AutoresDTO());
        }
        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AutoresDTO>(content);
                if (result != null)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");

                    await _httpClient.PostAsync("/api/Autores/deleteAutor", contenido);

                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
