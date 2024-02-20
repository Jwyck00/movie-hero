using Application.Models;

namespace Application.Mapping;

public static class MappingExtensions
{
    public static Task<IPaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber,
        int pageSize
    ) where TDestination : class =>
        PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

    public static Task<IPaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        PaginatedRequest request
    ) where TDestination : class =>
        PaginatedList<TDestination>.CreateAsync(queryable, request.PageNumber, request.PageSize);
}
