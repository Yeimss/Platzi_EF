using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Platzi_EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb2"), "Ocio, relajo, chill, sornerito, todo eso", "Actividades Personales", 50 },
                    { new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb7"), "Tareas pendientes", "Actividades Pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaFin", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("99abb2e2-7e5c-4595-bb51-d4596551632c"), new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb2"), "Hay que terminar ese anime mi rey", new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Terminar Re-zero" },
                    { new Guid("9ebebc8f-41e2-46f5-a4b5-2e75e0390e6f"), new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb7"), "Agua, gas, alcantarillado, luz e internet", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pago de servicios" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("99abb2e2-7e5c-4595-bb51-d4596551632c"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("9ebebc8f-41e2-46f5-a4b5-2e75e0390e6f"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb2"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("0ca42c9d-201e-42df-8587-0e7728d45fb7"));
        }
    }
}
