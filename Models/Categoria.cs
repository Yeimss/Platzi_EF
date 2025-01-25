using System.ComponentModel.DataAnnotations;

namespace Platzi_EF.Models;

public class Categoria
{
    [Key]
    public Guid CategoriaId { get; set; }
    [Required]
    [MaxLength(60)]
    public string Nombre { get; set; }
    [MaxLength(250)]
    public string Descripcion { get; set; }
    public virtual ICollection<Tarea> Tareas { get; set; }
}