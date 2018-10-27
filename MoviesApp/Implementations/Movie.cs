using MoviesApp.Interfaces;

namespace MoviesApp.Implementations
{
    public class Movie : IMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
    }
}