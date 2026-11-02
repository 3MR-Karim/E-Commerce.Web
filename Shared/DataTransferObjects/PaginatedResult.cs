namespace Shared.DataTransferObjects;

public class PaginatedResult<T>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int Count { get; set; }

    public IReadOnlyList<T> Data { get; set; } = Array.Empty<T>();

    public PaginatedResult(
        int pageIndex,
        int pageSize,
        int count,
        IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}