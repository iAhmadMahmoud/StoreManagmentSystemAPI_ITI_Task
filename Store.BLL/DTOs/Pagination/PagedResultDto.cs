namespace Store.BLL
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public PaginationMetadataDto Metadata { get; set; } = new();
    }
}
