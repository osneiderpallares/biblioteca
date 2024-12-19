using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Models;

namespace biblioteca.Controllers
{
    public class LibroController : Controller
    {      
        private readonly HttpClient _httpClient;

        public LibroController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44315/api");
        }

        // GET: Libro
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Libros/listLibros");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<LibroDTO>>(content);
                return View("Index", result);
            }            

            return View(new List<LibroDTO>());
        }
        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var response = await _httpClient.GetAsync($"/api/Libros/detailsLibro?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LibroDTO>(content);
                return View(result);
            }

            return View(new LibroDTO());
        }
        // GET: Libro/Create
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync("/api/Autores/listAutores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<AutoresDTO>>(content);
                var selectList = result.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Nombre
                }).ToList();
                ViewData["Autor"] = selectList;
            }
            return View();
        }
        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={libro.Autor}");
                if (response == null)
                {
                    ViewBag.ErrorMessage = "El autor no está registrado.";
                    var resp = await _httpClient.GetAsync("/api/Autores/listAutores");
                    if (resp.IsSuccessStatusCode)
                    {
                        var content = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IEnumerable<AutoresDTO>>(content);
                        var selectList = result.Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Nombre
                        }).ToList();

                        ViewData["Autor"] = selectList;
                    }                    
                    return View(libro);
                }
                var par = "numero de libros permitidos";
                var numMaxLibros = await _httpClient.GetAsync($"/api/Parametros/parametro?parametro={par}");
                var cont = await numMaxLibros.Content.ReadAsStringAsync();
                var parametro = JsonConvert.DeserializeObject<Parametros>(cont);
          
                var r = await _httpClient.GetAsync("/api/Libros/listLibros");
                var c = await r.Content.ReadAsStringAsync();
                var conteo = JsonConvert.DeserializeObject<IEnumerable<Libro>>(c);

                if (Convert.ToUInt32(parametro.Valor) <= conteo.Count())
                {
                    throw new NotImplementedException("No es posible registrar el libro, se alcanzó el máximo permitido.");
                }

                if (ModelState.IsValid)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");
                    var respuesta = await _httpClient.PostAsync("/api/Libros/save", contenido);
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
                return View(libro);
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
                return View(new LibroDTO());
            }
        }
        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync($"/api/Libros/detailsLibro?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var libro = JsonConvert.DeserializeObject<LibroDTO>(content);

            if (libro == null)
            {
                return NotFound();
            }
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
            return View(libro);
        }
        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/Autores/detailsAutores?id={libro.Autor}");
                    if (response == null)
                    {
                        ViewBag.ErrorMessage = "El autor no está registrado.";
                        var resp = await _httpClient.GetAsync("/api/Autores/listAutores");
                        if (resp.IsSuccessStatusCode)
                        {
                            var content = await resp.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<IEnumerable<AutoresDTO>>(content);
                            var selectList = result.Select(a => new SelectListItem
                            {
                                Value = a.Id.ToString(),
                                Text = a.Nombre
                            }).ToList();

                            ViewData["Autor"] = selectList;
                        }
                        return View(libro);
                    }

                    var contenido = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");
                    var respuesta = await _httpClient.PostAsync("/api/Libros/update", contenido);
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
                    var response = await _httpClient.GetAsync($"/api/Libros/detailsLibro?id={id}");
                    var content = await response.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<LibroDTO>(content);
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
            return View(libro);
        }       

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {           
            var response = await _httpClient.GetAsync($"/api/Libros/detailsLibro?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LibroDTO>(content);
                if (result == null)
                {
                    return NotFound();
                }

                return View(result);
            }

            return View(new LibroDTO());          
        }
        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Libros/detailsLibro?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LibroDTO>(content);
                if (result != null)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");

                    await _httpClient.PostAsync("/api/Libros/deleteLibro", contenido);
                                      
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
