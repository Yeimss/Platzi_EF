using Microsoft.EntityFrameworkCore;

namespace Platzi_EF.Models.Context;

public class TareasContext : DbContext 
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("0ca42c9d-201e-42df-8587-0e7728d45fb7"), Nombre = "Actividades Pendientes", Descripcion= "Tareas pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("0ca42c9d-201e-42df-8587-0e7728d45fb2"), Nombre = "Actividades Personales", Descripcion= "Ocio, relajo, chill, sornerito, todo eso", Peso = 50});


        modelBuilder.Entity<Categoria>(categoria => {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(60);
            categoria.Property(p => p.Descripcion).HasMaxLength(250);
            categoria.Property(p => p.Peso);
            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea{ TareaId = Guid.Parse("9ebebc8f-41e2-46f5-a4b5-2e75e0390e6f"), CategoriaId = Guid.Parse("0ca42c9d-201e-42df-8587-0e7728d45fb7"), PrioridadTarea = Prioridad.Media, Descripcion = "Agua, gas, alcantarillado, luz e internet", Titulo = "Pago de servicios", FechaFin = new DateTime(2025, 1, 25) });
        tareasInit.Add(new Tarea{ TareaId = Guid.Parse("99abb2e2-7e5c-4595-bb51-d4596551632c"), CategoriaId = Guid.Parse("0ca42c9d-201e-42df-8587-0e7728d45fb2"), PrioridadTarea = Prioridad.Baja, Descripcion = "Hay que terminar ese anime mi rey", Titulo = "Terminar Re-zero", FechaFin = new DateTime(2025, 1, 30) });
        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");
            tarea.HasKey(p=> p.TareaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(60);
            tarea.Property(p => p.Descripcion).HasMaxLength(250);
            tarea.Property(p => p.PrioridadTarea).IsRequired();
            tarea.Property(p => p.FechaCreacion).HasDefaultValueSql("GETDATE()");
            tarea.Property(p => p.FechaFin);
            tarea.Ignore(p => p.Resumen);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.HasData(tareasInit);
        });
    }
}