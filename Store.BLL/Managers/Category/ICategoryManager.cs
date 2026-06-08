using Store.Common;

namespace Store.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<GetCategoryDtos>>> GetCategoriesAsync();
        Task<GeneralResult<GetCategoryDtos?>> GetCategoryByIdAsync(int id);
        Task<GeneralResult<GetCategoryDtos>> InsertAsync(CreateCategoryDtos catVM);

        Task<GeneralResult<GetCategoryDtos>> EditAsync(GetCategoryDtos catVM);
        Task<GeneralResult> DeleteAsync(int id);
    }
}
