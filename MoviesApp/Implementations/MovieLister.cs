using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.Interfaces;

namespace MoviesApp.Implementations
{
    public class MovieLister : IMovieLister
    {
        private readonly IMovieFinder _finder;

        public MovieLister(IMovieFinder finder)
        {
            _finder = finder;
        }

        public void DisplayMovies()
        {
            foreach (var movie in _finder.GetAll())
            {
                Console.WriteLine(movie.Id + ": " + movie.Name + " by " + movie.Producer);
            }
        }

        public IList<IMovie> MoviesWithProducer(string producer)
        {
            return _finder.GetAll().Where(movie => movie.Producer == producer).ToList();
        }
    }
}