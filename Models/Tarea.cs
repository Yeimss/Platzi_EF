using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platzi_EF.Models;

public class Tarea{
    [Key]
    public Guid TareaId { get; set; }
    [ForeignKey("Fk_CategoriaId")]
    public Guid CategoriaId { get; set; }
    [Required]
    [MaxLength(60)]
    public string Titulo { get; set; }
    [MaxLength(250)]
    public string Descripcion { get; set; }
    [Required]
    public Prioridad PrioridadTarea { get; set; }
    public DateTime FechaCreacion { get; set; }
    public virtual Categoria Categoria { get; set; }
    [NotMapped]
    public string Resumen { get; set; }
}
public enum Prioridad{
    Baja, 
    Media,
    Alta
}