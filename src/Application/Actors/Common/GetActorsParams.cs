using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Actors.Common;

public class GetActorsParams : PaginationParams
{
    [FromQuery(Name = "q")]
    public string? SearchQuery { get; set; }

    [FromQuery(Name = "movie-ids")]
    public IList<Guid>? MovieIds { get; set; }
}
