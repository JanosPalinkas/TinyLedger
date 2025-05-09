namespace TinyLedger.Api.Dtos
{
    public class PaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = [];
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
