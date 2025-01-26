using System.Xml.XPath;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Platzi_EF.Models;
using Platzi_EF.Models.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(connectionString:  builder.Configuration.GetConnectionString("cnTareas"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => {
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) => {
    return Results.Ok(new 
    {
        mensaje = "Datos encontrados",
        estado = true, 
        cantidadDatos =  dbContext.Tareas.Count(), 
        data = dbContext.Tareas
        .Include(p => p.Categoria)
        .ToList()
    });
});

app.MapGet("/api/tareas/{titulo}", async ([FromServices] TareasContext dbContext,[FromRoute] string titulo) => {
    return Results.Ok(new 
    {
        mensaje = "Datos encontrados",
        estado = true, 
        data = dbContext.Tareas.FirstOrDefault(t => t.Titulo.Contains(titulo))
    });
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext,[FromBody] Tarea tarea) => {
    tarea.TareaId = Guid.NewGuid();

    //await dbContext.AddAsync(tarea);
    await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new {
        mensaje = "Tarea agregada correctamente",
        estad = true
    });
});
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext,[FromBody] Tarea tarea, [FromRoute] Guid id) => {

    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual != null){
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;
        tareaActual.FechaFin = tarea.FechaFin;
        await dbContext.SaveChangesAsync();
        return Results.Ok(new {
            mensaje = "Tarea actualizada correctamente",
            estad = true
        });
    }else{
        return Results.NotFound(new {
            mensaje = "Tarea no encontrada",
            estado = false
        });
    }
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) => {
    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual != null){
        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok(new {
            mensaje = "Tarea eliminada correctamente",
            estad = true
        });
    }else{
        return Results.NotFound(new {
            mensaje = "Tarea no encontrada",
            estado = false
        });
    }
});
app.Run();