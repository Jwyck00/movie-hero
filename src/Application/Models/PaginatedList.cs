using Microsoft.EntityFrameworkCore;

namespace Application.Models;

public interface IPaginatedList<TItems>
{
    IReadOnlyCollection<TItems> Items { get; }
    int PageNumber { get; }
    int TotalPages { get; }
    int TotalCount { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}

public class PaginatedList<TItems> : IPaginatedList<TItems>
{
    public IReadOnlyCollection<TItems> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<TItems> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<IPaginatedList<TItems>> CreateAsync(
        IQueryable<TItems> source,
        int pageNumber,
        int pageSize
    )
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<TItems>(items, count, pageNumber, pageSize);
    }
}
