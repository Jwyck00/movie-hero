namespace Presentation.Api.Contracts.Common;

public interface IPaginationParams
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
}