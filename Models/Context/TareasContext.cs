using Microsoft.EntityFrameworkCore;

namespace Platzi_EF.Models.Context;

public class TareasContext : DbContext 
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(categoria => {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(60);
            categoria.Property(p => p.Descripcion).HasMaxLength(250);
        });

        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");
            tarea.HasKey(p=> p.TareaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(60);
            tarea.Property(p => p.Descripcion).HasMaxLength(250);
            tarea.Property(p => p.PrioridadTarea).IsRequired();
            tarea.Ignore(p => p.Resumen);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.FechaCreacion).HasDefaultValueSql("GETDATE()");
        });
    }
}