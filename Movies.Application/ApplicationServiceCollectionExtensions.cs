using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Database;
using Movies.Application.Repositories;
using Movies.Application.Services;

namespace Movies.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IRatingService, RatingService>();
        services.AddSingleton<IMovieService, MovieService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IRatingRepository, RatingRepository>();
        services.AddSingleton<IMovieRepository, MovieRepository>();
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgSqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }

    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, InMemoryMovieRepository>();
        return services;
    }
}