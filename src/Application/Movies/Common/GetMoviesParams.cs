using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Movies.Common;

public class GetMoviesParams : PaginationParams
{
    [FromQuery(Name = "q")]
    public string? SearchQuery { get; set; }
}
