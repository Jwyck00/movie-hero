namespace Application.Common.Models;

public interface IPaginationParams
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
}
