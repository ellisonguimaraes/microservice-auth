namespace MicroserviceAuth.Domain.Pagination;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public PagedList()
    {
         
    }

    public PagedList(IEnumerable<T> source, int pageSize, int currentPage)
    {
        TotalCount = source.Count();
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

        AddRange(source.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
    }
}
