using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoviesApp.Interfaces;
using Newtonsoft.Json;

namespace MoviesApp.Implementations
{
    public class MovieFinderJson : IMovieFinder
    {
        private readonly string _file;

        public MovieFinderJson(string file)
        {
            _file = file;
        }

        public IMovie FindById(int id)
        {
            var movies = _getMovies();
            return movies.FirstOrDefault(movie => movie.Id == id);
        }

        public IMovie FindByProducer(string producer)
        {
            var movies = _getMovies();
            return movies.FirstOrDefault(movie => movie.Producer == producer);
        }

        public IEnumerable<IMovie> GetAll()
        {
            return _getMovies();
        }

        private IEnumerable<IMovie> _getMovies()
        {
            using (var fs = new FileStream(_file, FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                var content = sr.ReadToEnd();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
                return movies;
            }
        }
    }
}