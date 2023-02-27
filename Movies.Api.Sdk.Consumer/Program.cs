using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Api.Sdk.Consumer;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

var services = new ServiceCollection();
services
    .AddHttpClient()
    .AddSingleton<AuthTokenProvider>()
    .AddRefitClient<IMoviesApi>(s => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async () => await s.GetRequiredService<AuthTokenProvider>().GetTokenAsync()
    })
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:5001"));

var provider = services.BuildServiceProvider();
var moviesApi = provider.GetRequiredService<IMoviesApi>();

var newMovie = await moviesApi.CreateMovieAsync(new CreateMovieRequest
{
    Title = "The Fast And The Furious 64",
    YearOfRelease = 2023,
    Genres = new[] { "Action" }
});

var updatedMovie = await moviesApi.UpdateMovieAsync(newMovie.Id, new UpdateMovieRequest
{
    Title = "The Fast And The Furious 65",
    YearOfRelease = 2022,
    Genres = new[] { "Action", "Adventure" }
});

var movie = await moviesApi.GetMovieAsync(newMovie.Id.ToString());
Console.WriteLine(JsonSerializer.Serialize(movie));

await moviesApi.DeleteMovieAsync(newMovie.Id);

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