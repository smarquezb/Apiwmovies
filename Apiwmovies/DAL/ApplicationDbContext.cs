using Apiwmovies.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Apiwmovies.DAL
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }
        // seccion para crear el dbset de las entidades o modelos
        public DbSet<Category> Categories { get; set; }


    }
}
