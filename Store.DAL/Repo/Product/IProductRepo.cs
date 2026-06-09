using Store.Common;

namespace Store.DAL
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetByIdWithCategoryAsync(int _prodId);
        Task<PagedResult<Product>> GetAllPaginationAsync(PaginationParameters? paginationParameters, ProductFilterParameters? filterParameters);



    }
}
