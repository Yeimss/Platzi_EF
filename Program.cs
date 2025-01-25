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

app.MapGet("/api/tareas/{titulo}", async ([FromServices] TareasContext dbContext, string titulo) => {
    return Results.Ok(new 
    {
        mensaje = "Datos encontrados",
        estado = true, 
        data = dbContext.Tareas.FirstOrDefault(t => t.Titulo.Contains(titulo))
    });
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, Tarea tarea) => {
    tarea.TareaId = Guid.NewGuid();

    //await dbContext.AddAsync(tarea);
    await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new {
        mensaje = "Tarea agregada correctamente",
        estad = true
    });
});
app.Run();