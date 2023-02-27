using Movies.Api.Sdk;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

var moviesApi = RestService.For<IMoviesApi>("https://localhost:5001");

var movie = await moviesApi.GetMovieAsync("home-fries-1998");
Console.WriteLine(JsonSerializer.Serialize(movie));

var movies = await moviesApi.GetMoviesAsync(new GetAllMoviesRequest
{
    Title = null,
    Year = null,
    SortBy = null,
    Page = 1,
    PageSize = 3
});
foreach(var movieResponse in movies.Items)
{
    Console.WriteLine(JsonSerializer.Serialize(movieResponse));
}