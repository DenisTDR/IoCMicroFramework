using System.Collections.Generic;

namespace MoviesApp.Interfaces
{
    public interface IMovieFinder
    {
        IMovie FindById(int id);
        IMovie FindByProducer(string producer);
        IEnumerable<IMovie> GetAll();
    }
}