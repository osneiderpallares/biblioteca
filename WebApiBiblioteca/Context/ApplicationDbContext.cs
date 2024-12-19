using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Models;

namespace WebApiBiblioteca.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Autore> Autores { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autore>(entity =>
            {
                entity.ToTable("autores");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Ciudad)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");
                entity.Property(e => e.Correo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("correo");
                entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("libros");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Autor).HasColumnName("autor");
                entity.Property(e => e.Año).HasColumnName("año");
                entity.Property(e => e.Genero)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("genero");
                entity.Property(e => e.Paginas).HasColumnName("paginas");
                entity.Property(e => e.Titulo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.AutorNavigation).WithMany(p => p.Libros)
                    .HasForeignKey(d => d.Autor)
                    .HasConstraintName("FK_libros_autores");
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.ToTable("parametros");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Parametro)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("parametro");
                entity.Property(e => e.Valor)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("valor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
