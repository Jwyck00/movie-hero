using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Models;

public class PaginationParams : IPaginationParams
{
    [DefaultValue(1)]
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; init; }

    [DefaultValue(10)]
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; init; }
}
