namespace MoviesApp.Interfaces
{
    public interface IMovie
    {
        int Id { get; set; }
        string Name { get; set; }
        string Producer { get; set; }
    }
}