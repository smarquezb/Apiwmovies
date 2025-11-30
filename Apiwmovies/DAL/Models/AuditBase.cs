using System.ComponentModel.DataAnnotations;

namespace API.P.Movies.DAL.Models
{
    public class AuditBase
    {
        [Key] //Decorator
        // Este data annotation indica que esta propiedad es la clave primaria
        // The virtual is like a inherits (herencia) 
        public virtual int Id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }  // ? indica que esta propiedad es nullable (acepta null)
    }
}


