using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoviesApp.Interfaces;

namespace MoviesApp.Implementations
{
    public class MovieFinderTxt : IMovieFinder
    {
        private readonly string _file;

        public MovieFinderTxt(string file)
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
            var list = new List<IMovie>();

            using (var fs = new FileStream(_file, FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                var n = int.Parse(sr.ReadLine());
                for (var i = 0; i < n; i++)
                {
                    var movie = new Movie();
                    movie.Id = int.Parse(sr.ReadLine());
                    movie.Name = sr.ReadLine();
                    movie.Producer = sr.ReadLine();
                    list.Add(movie);
                }
            }

            return list;
        }
    }
}