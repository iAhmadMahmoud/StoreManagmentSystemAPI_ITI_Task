using AutoMapper;
using FluentValidation;
using Store.Common;
using Store.DAL;

namespace Store.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductDtos> _ceateProductValidator;
        private readonly IValidator<EditProductDtos> _editProductValidator;
        private readonly IErrorMapper _errorMapper;
        private readonly IMapper _mapper;

        public ProductManager
            (
            IUnitOfWork unitOfWork,
            IValidator<CreateProductDtos> ceateProductValidator,
            IValidator<EditProductDtos> editProductValidator,
            IErrorMapper errorMapper,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _ceateProductValidator = ceateProductValidator;
            _editProductValidator = editProductValidator;
            _errorMapper = errorMapper;
            _mapper = mapper;
        }

        public async Task<GeneralResult<PagedResultDto<GetProductDtos>>> GetProductsPaginationAsync
            (
                PaginationParameters? paginationParameters,
                ProductFilterParameters? filterParameters
            )
        {
            var prods = await _unitOfWork.ProductRepo.GetAllPaginationAsync(paginationParameters,filterParameters);

           
            var items = _mapper.Map<PagedResultDto<GetProductDtos>>(prods);
            return GeneralResult<PagedResultDto<GetProductDtos>>.SuccessResult(items);
        }
        public async Task<GeneralResult<IEnumerable<GetProductDtos>>> GetProductsAsync()
        {
            var prods = await _unitOfWork.ProductRepo.GetAllWithCategoryAsync();
     
            var products = _mapper.Map<List<GetProductDtos>>( prods );

            return GeneralResult<IEnumerable<GetProductDtos>>.SuccessResult(products);
        }

        public async Task<GeneralResult<GetProductDtos>> GetProductByIdAsync(int id)
        {
            var prod = await _unitOfWork.ProductRepo.GetByIdWithCategoryAsync(id);
            if (prod == null)
            {
                return GeneralResult<GetProductDtos>.NotFoundResult($"Product with {id} not found.");
            }

            var product = _mapper.Map<GetProductDtos>(prod);
            return GeneralResult<GetProductDtos>.SuccessResult(product);

        }

        public async Task<GeneralResult<GetProductDtos>> InsertAsync(CreateProductDtos prod)
        {
            var validateResult = await _ceateProductValidator.ValidateAsync(prod);
            if (!validateResult.IsValid)
            {
                var errors = _errorMapper.MapError(validateResult);
                return GeneralResult<GetProductDtos>.FailResult(errors);
            }

            var cat = _unitOfWork.CategoryRepo.GetByIdAsync(prod.CategoryId);
            if (cat == null)
            {
                return GeneralResult<GetProductDtos>.NotFoundResult("Category not found.");
            }

            var product = _mapper.Map<Product>(prod);
            _unitOfWork.ProductRepo.Add(product);
            await _unitOfWork.SaveChangesAsync();

            var prodDto = new GetProductDtos
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                
                Count = product.Count,
                Description = product.Description,
                Category = product.Category.Name

            };

            return GeneralResult<GetProductDtos>.SuccessResult(prodDto,"Product created Successfully.");
        }

        public async Task<GeneralResult<EditProductDtos>> EditAsync(EditProductDtos prodVM)
        {
            var validate = await _editProductValidator.ValidateAsync(prodVM);
            if (!validate.IsValid)
            {
                var errors = _errorMapper.MapError(validate);
                 
                return GeneralResult<EditProductDtos>.FailResult(errors);
            }
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(prodVM.Id);
            if (product is null)
            {
                return GeneralResult<EditProductDtos>.NotFoundResult($"Product with id {prodVM.Id} Not found.");
            }
            var cat = await _unitOfWork.CategoryRepo.GetByIdAsync(prodVM.CategoryId);
            if (cat is null)
            {
                return GeneralResult<EditProductDtos>.NotFoundResult("Category Not found.");
            }

            product.Title = prodVM.Title;
            product.Price = prodVM.Price;
            product.Count = prodVM.Count;
            product.Description = prodVM.Description;
            product.CategoryId = prodVM.CategoryId;

            await _unitOfWork.SaveChangesAsync();

            var res = _mapper.Map<EditProductDtos>(product);
            return GeneralResult<EditProductDtos>.SuccessResult(res, "Updated product successfully.");
        }

        public async Task<GeneralResult> DeleteAsync(int id)
        {
            var prod = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            if (prod == null)
            {
                return GeneralResult.NotFoundResult($"Product With ID {id} Not Found.");
            }
            _unitOfWork.ProductRepo.Delete(prod);
            await _unitOfWork.SaveChangesAsync();

            return GeneralResult.SuccessResult("Product deleted Successfully");
        }
    }
}