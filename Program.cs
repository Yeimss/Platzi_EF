using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platzi_EF.Models.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>("Data source=YEIMS;Initial Catalog=TareasDb;user id=sa;password=1234;TrustServerCertificate=True");
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => {
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");
});

app.Run();
