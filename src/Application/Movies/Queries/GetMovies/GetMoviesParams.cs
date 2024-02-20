using Application.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace Application.Contracts.Movies;

public class GetMoviesParams : PaginationParams
{
    [FromQuery(Name = "q")]
    public string? SearchQuery { get; set; }
}
