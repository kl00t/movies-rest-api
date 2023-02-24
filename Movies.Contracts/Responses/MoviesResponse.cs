namespace Movies.Contracts.Responses;

public class MoviesResponse : PagedResponse<MovieResponse>
{
}

public class PagedResponse<TResponse>
{
    public IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();

    public required int PageSze { get; init;}

    public required int Page { get; init;}

    public required int Total { get; init; }

    public bool HasNextPage => Total > (Page * PageSze);
}