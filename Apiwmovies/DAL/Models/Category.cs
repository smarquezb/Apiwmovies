using System.ComponentModel.DataAnnotations;

namespace API.P.Movies.DAL.Models
{
    public class Category : AuditBase  // Category inherits (hereda) from AuditBase with : AuditBase, inheriting this properties Id, CreatedDate, and ModifiedDate, has also its own property Name
    {
        [Required] // Este data annotation indica que el campo es obligatorio
        [Display(Name = "Nombre de la categoría")] // Me sirve para personalizar el nombre que se muestra en las vistas o mensajes de error
        public string Name { get; set; }
    }
}


