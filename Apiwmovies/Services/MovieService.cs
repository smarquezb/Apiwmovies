using API.P.Movies.DAL.Models;
using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Repository.IRepository;
using API.P.Movies.Services.IServices;
using AutoMapper;

namespace API.P.Movies.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }


        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto)
        {
            //validamos si la movie ya existe
            var movieExists = await _movieRepository.MovieExistsByNameAsync(movieCreateUpdateDto.Name);
            if (movieExists)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre de '{movieCreateUpdateDto.Name}'");
            }

            //mapeamos el dto a la entidad
            var movie = _mapper.Map<Movie>(movieCreateUpdateDto);

            //creamos la movie en el repositorio (base de datos)
            var movieCreated = await _movieRepository.CreateMovieAsync(movie);

            if (!movieCreated)
            {
                throw new Exception("No se pudo crear la pelicula");
            }

            //mapeamos la entidad a dto
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            //verificar si la movie existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontró la pelicula con ID: '{id}'");
            }

            //eliminamos la movie del repositorio (base de datos)
            var movieDeleted = await _movieRepository.DeleteMovieAsync(id);

            if (!movieDeleted)
            {
                throw new Exception("Ocurrió un error al eliminar la pelicula");
            }

            return movieDeleted;
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id); //obtengo la movie del repositorio

            if (movie == null)
            {
                throw new InvalidOperationException($"No se encontró la pelicula con ID: '{id}'");
            }

            return _mapper.Map<MovieDto>(movie); //mapeo la movies a lista de MovieDto para no exponer mi modelo
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync(); //obtengo las movies del repositorio
            return _mapper.Map<ICollection<MovieDto>>(movies); //mapeo la lista de movies a lista de MovieDto para no exponer mi modelo
        }

        public async Task<bool> MovieExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovieExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto dto, int id)
        {
            //validamos si la movie ya existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontró la pelicula con ID: '{id}'");
            }

            var nameExists = await _movieRepository.MovieExistsByNameAsync(dto.Name);

            if (nameExists)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre de '{dto.Name}'");
            }

            //mapeamos el dto a la entidad
            _mapper.Map(dto, movieExists);

            //actualizamos la movie en el repositorio (base de datos)
            var movieUpdated = await _movieRepository.UpdateMovieAsync(movieExists);

            if (!movieUpdated)
            {
                throw new Exception("Ocurrió un error al actualizar la pelicula");
            }

            //retornamos el dto actualizado
            return _mapper.Map<MovieDto>(movieExists);
        }
    }
}

