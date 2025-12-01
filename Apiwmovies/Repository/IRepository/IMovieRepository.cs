using API.P.Movies.DAL.Models;

namespace API.P.Movies.Repository.IRepository
{
    public interface IMovieRepository
    {
        //7 firmas de los metodos
        Task<ICollection<Movie>> GetMoviesAsync(); //retorna las peliculas 
        Task<Movie> GetMovieAsync(int id); //retorna pelicula por id
        Task<bool> MovieExistsByIdAsync(int id); //Me dice si una pelicula existe por su Id
        Task<bool> MovieExistsByNameAsync(string name); //si existe pelicula por su nombre
        Task<bool> CreateMovieAsync(Movie movie); //crear pelicula
        Task<bool> UpdateMovieAsync(Movie movie); //Me actualiza una pelicula, puedo actualizar el nombre, duracion, descripcion, clasificacion y la fecha de actualizacion
        Task<bool> DeleteMovieAsync(int id); //Me elimina una pelicula

    }
}

