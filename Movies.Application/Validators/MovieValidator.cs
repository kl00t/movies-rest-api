using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Services;

namespace Movies.Application.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
	private readonly IMovieRepository _movieRepository;

	public MovieValidator(IMovieRepository movieRepository)
	{
		_movieRepository = movieRepository;

		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.Title)
			.NotEmpty()
			.WithMessage("The movie must have a title.");

        RuleFor(x => x.Slug)
			.MustAsync(ValidateSlug)
			.WithMessage("This movie already exists.");

        RuleFor(x => x.YearOfRelease)
			.LessThanOrEqualTo(DateTime.UtcNow.Year)
			.NotEmpty();

		RuleFor(x => x.Genres)
			.NotEmpty()
			.WithMessage("The movie must have a genre.");
	}

	private async Task<bool> ValidateSlug(Movie movie, string slug, CancellationToken token = default)
	{
		var existingMovie = await _movieRepository.GetBySlugAsync(slug, cancellationToken: token);
		if (existingMovie is not null)
		{
			return existingMovie.Id == movie.Id;
		}

		return existingMovie is null;
	}
}