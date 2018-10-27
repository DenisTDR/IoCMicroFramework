using System.Collections.Generic;

namespace MoviesApp.Interfaces
{
    public interface IMovieLister
    {
        void DisplayMovies();

        IList<IMovie> MoviesWithProducer(string producer);
    }
}