namespace Store.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public PaginationMetadata Metadata { get; set; } = new();
    }
}
