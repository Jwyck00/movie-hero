using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Application.Contracts.Common;

public class PaginationParams : IPaginationParams
{
    [DefaultValue(1)]
    [FromQuery(Name = "page-number")]
    public int PageNumber { get; init; }

    [DefaultValue(10)]
    [FromQuery(Name = "page-size")]
    public int PageSize { get; init; }
}
