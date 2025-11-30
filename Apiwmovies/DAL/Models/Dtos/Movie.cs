using System.ComponentModel.DataAnnotations;

namespace API.P.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        [Display(Name = "Nombre de la categoría")]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }
        public string? Description { get; set; }

        [Required]
        public string Clasification { get; set; }
    }
}