namespace MicroserviceAuth.Domain.Pagination;

public class PaginationParameters
{
    const int MAX_PAGE_SIZE = 50;

    private int _pageSize = 10;

    /// <summary>
    /// Page number
    /// </summary>
    /// <example>2</example>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page size
    /// </summary>
    /// <example>10</example>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }

    public PaginationParameters() 
    {
    }

    public PaginationParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}