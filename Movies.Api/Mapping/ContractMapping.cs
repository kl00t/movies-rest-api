using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Movies.Api.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
    }

    public static IEnumerable<Movie> MapToMovies(this CreateBulkMoviesRequest[] request)
    {
        List<Movie> response = new();
        response.AddRange(from movie in request
                          select new Movie
                          {
                              Id = movie.Id,
                              Title = movie.Title,
                              YearOfRelease = movie.YearOfRelease,
                              Genres = movie.Genres.ToList()
                          });
        return response.AsEnumerable();
    }

    public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
    {
        return new Movie
        {
            Id = id,
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
        };
    }

    public static MovieResponse MapToResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Slug = movie.Slug,
            Rating= movie.Rating,
            UserRating = movie.UserRating,
            YearOfRelease = movie.YearOfRelease,
            Genres = movie.Genres,
        };
    }

    public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse
        {
            Items = movies.Select(MapToResponse)
        };
    }

    public static IEnumerable<MovieRatingResponse> MapToResponse(this IEnumerable<MovieRating> ratings)
    {
        return ratings.Select(x => new MovieRatingResponse
        {
            MovieId = x.MovieId,
            Rating = x.Rating,
            Slug= x.Slug
        });
    }

    public static GetAllMoviesOptions MapToOptions(this GetAllMoviesRequest request)
    {
        return new GetAllMoviesOptions
        {
            Title = request.Title,
            YearOfRelease = request.Year,
        };
    }

    public static GetAllMoviesOptions WithUserId(this GetAllMoviesOptions options, Guid? userId)
    {
        options.UserId = userId;
        return options;
    }
}