using System.ComponentModel;
using Application.Models;

namespace Presentation.Api.Contracts.Common;

public class PaginationParams : IPaginationParams
{
    [DefaultValue(1)]
    public int PageNumber { get; init; }

    [DefaultValue(10)]
    public int PageSize { get; init; }
}
