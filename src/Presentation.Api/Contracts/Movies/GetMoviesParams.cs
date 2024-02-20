using Presentation.Api.Contracts.Common;

namespace Presentation.Api.Contracts.Movies;

public class GetMoviesParams : PaginationParams
{
    public string? Q { get; set; }
}
