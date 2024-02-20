namespace Application.Contracts.Common;

public interface IPaginationParams
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
}
