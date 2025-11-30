using API.P.Movies.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.P.Movies.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // Metodo constructor que tiene el mismo nombre que la clase, <> notacion diamante
        {

        }

        //Sección para crear el dbset de las entidades o modelos
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}