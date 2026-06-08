using Microsoft.AspNetCore.Http;
using Store.Common;

namespace Store.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<IEnumerable<GetProductDtos>>> GetProductsAsync();
        Task<GeneralResult<GetProductDtos>> GetProductByIdAsync(int id);
        Task<GeneralResult<GetProductDtos>> InsertAsync(CreateProductDtos prod);
        Task<GeneralResult<EditProductDtos>> EditAsync(EditProductDtos prodVM);
        Task<GeneralResult> DeleteAsync(int id);
        
        
        
    }
}
