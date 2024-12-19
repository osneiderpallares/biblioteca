using System.ComponentModel.DataAnnotations;
using WebApiBiblioteca.Models;

namespace WebApiBiblioteca.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(150, ErrorMessage = "El valor no puede superar los 150 caracteres")]
        public string? Titulo { get; set; }

        public int? Año { get; set; }
        [Required(ErrorMessage = "El género es obligatorio")]
        public string? Genero { get; set; }

        public int? Paginas { get; set; }
        [Required(ErrorMessage = "El autor es obligatorio")]
        public int? Autor { get; set; }

        public virtual Autore? AutorNavigation { get; set; }
    }
}
