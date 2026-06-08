using Store.Common;
using Store.DAL;


namespace Store.BLL
{
    public class CategoryManager:ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult<IEnumerable<GetCategoryDtos>>> GetCategoriesAsync()
        {
            var cat = await _unitOfWork.CategoryRepo.GetAllAsync();
            var catDtos = cat.Select(c => new GetCategoryDtos
            {
                Id = c.Id,
                Name = c.Name,
            });
       
            return GeneralResult<IEnumerable<GetCategoryDtos>>.SuccessResult(catDtos);
        }

        public async Task<GeneralResult<GetCategoryDtos?>> GetCategoryByIdAsync(int id)
        {
            var cat = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            if (cat == null)
            {
                return GeneralResult<GetCategoryDtos?>.NotFoundResult($"Category With ID {id} Not Found");
            }
            var catDto = new GetCategoryDtos
            {
                Id = cat.Id,
                Name = cat.Name
            };
            return GeneralResult<GetCategoryDtos?>.SuccessResult(catDto);
        }

        public async Task<GeneralResult<GetCategoryDtos>> InsertAsync(CreateCategoryDtos catVM)
        {
            var newCat = new Category
            {
                Name = catVM.Name
            };
            _unitOfWork.CategoryRepo.Add(newCat);
            await _unitOfWork.SaveChangesAsync();
            var res = new GetCategoryDtos
            {
                Id = newCat.Id,
                Name = newCat.Name
            };
            return GeneralResult<GetCategoryDtos>.SuccessResult(res,"Category Created Successfully");
        }

        public async Task<GeneralResult<GetCategoryDtos>> EditAsync(GetCategoryDtos catVM)
        {
            var cat = await _unitOfWork.CategoryRepo.GetByIdAsync(catVM.Id);
            if(cat == null)
            {
                return GeneralResult<GetCategoryDtos>.NotFoundResult($"Category With ID {catVM.Id}");
            }
            cat.Name = catVM.Name;
            await _unitOfWork.SaveChangesAsync();

            var res = new GetCategoryDtos
            {
                Id = cat.Id,
                Name = cat.Name
            };

            return GeneralResult<GetCategoryDtos>.SuccessResult(res, "Category updated Successfully");

        }

        public async Task<GeneralResult> DeleteAsync(int id)
        {
            var cat = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            if (cat == null)
            {
                return GeneralResult.NotFoundResult($"This Category With Id {id} not found.");
            }
            _unitOfWork.CategoryRepo.Delete(cat);
            await _unitOfWork.SaveChangesAsync();

            return GeneralResult.SuccessResult("Category Deleted Successfully");
        }

    }
}
