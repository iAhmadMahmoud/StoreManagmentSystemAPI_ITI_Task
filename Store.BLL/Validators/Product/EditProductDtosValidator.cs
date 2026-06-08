using FluentValidation;
using Store.DAL;

namespace Store.BLL
{
    public class EditProductDtosValidator : AbstractValidator<EditProductDtos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditProductDtosValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Name Is Required.")
                .WithErrorCode("ERR-01")
                .WithMessage("Title must not exceed 100 characters.")
                .WithErrorCode("ERR-02")
                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters long.")
                .WithErrorCode("ERR-03")
                .MustAsync(CheckTitleIsUnique)
                .WithMessage("Title must be unique. A Product with the same Title already exists.")
                .WithErrorCode("ERR-04");
            RuleFor(e => e.Description)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Description must be at least 3 characters long.")
                .WithErrorCode("ERR-05");
            RuleFor(e => e.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.")
                .WithErrorCode("ERR-06");
            RuleFor(p => p.Count)
                .GreaterThan(0)
                .WithMessage("Count must be greater than 0.")
                .WithErrorCode("ERR-07");
            RuleFor(p => p.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.")
                .WithErrorCode("ERR-08");

        }
        private async Task<bool> CheckTitleIsUnique(string title, CancellationToken cancellationToken)
        {
            var prod = await _unitOfWork.ProductRepo.GetAllGenericAsunc(e => e.Title == title);
            var isUnique = !prod.Any();
            return isUnique;
        }
    }
}
