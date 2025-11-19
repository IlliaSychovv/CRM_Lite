namespace CRM_Lite.Application.DTO.Pagination;

public record PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}