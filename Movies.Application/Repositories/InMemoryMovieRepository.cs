using Movies.Application.Models;

namespace Movies.Application.Repositories;

public class InMemoryMovieRepository : IMovieRepository
{
    private readonly List<Movie> _movies = new();

	public InMemoryMovieRepository()
	{

    }

    public Task<bool> CreateAsync(Movie movie)
    {
        _movies.Add(movie);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var removedCount = _movies.RemoveAll(x => x.Id == id);
        var movieRemoved = removedCount> 0;
        return Task.FromResult(movieRemoved);
    }

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = _movies.AsEnumerable();
        return Task.FromResult(movies);
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        var movie = _movies.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(movie);
    }

    public Task<Movie?> GetBySlugAsync(string slug)
    {
        var movie = _movies.SingleOrDefault(x => x.Slug == slug);
        return Task.FromResult(movie);
    }

    public Task<bool> UpdateAsync(Movie movie)
    {
        var movieIndex = _movies.FindIndex(x => x.Id == movie.Id);
        if (movieIndex == -1)
        {
            return Task.FromResult(false);
        }

        _movies[movieIndex] = movie;
        return Task.FromResult(true);
    }
}