using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models;

public partial class Autore
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(150, ErrorMessage = "El valor no puede superar los 150 caracteres")]
    public string? Nombre { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
    [Required(ErrorMessage = "La ciudad es obligatoria")]
    public string? Ciudad { get; set; }
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress]
    [StringLength(150, ErrorMessage = "El valor no puede superar los 150 caracteres")]
    public string? Correo { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
