using FluentValidation;
using Store.Common;

namespace Store.BLL
{
    // FIX: Removed 'static' from the class definition
    public class UploadImageManager : IUploadImageManager // Optional: Implement an interface for DI registration
    {
        private readonly IValidator<ImageUploadDto> _validator;
        private readonly IErrorMapper _errorMapper;

        // Constructor works perfectly now that the class is an instance class
        public UploadImageManager(IValidator<ImageUploadDto> validator, IErrorMapper errorMapper)
        {
            _validator = validator;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<ImageUploadResultDto>> UploadAsync(
             ImageUploadDto imageUploadDto,
             string basePath,
             string? schema,
             string? host)
        {
            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(host))
            {
                return GeneralResult<ImageUploadResultDto>.FailResult("Missing schema or host");
            }

            // Validation
            var result = await _validator.ValidateAsync(imageUploadDto);
            if (!result.IsValid)
            {
                var errors = _errorMapper.MapError(result);
                return GeneralResult<ImageUploadResultDto>.FailResult(errors);
            }

            var file = imageUploadDto.File;
            var extension = Path.GetExtension(file.FileName).ToLower();
            var cleanName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-").ToLower();
            var newFileName = $"{cleanName}-{Guid.NewGuid()}{extension}";

            const string folderName = "Files";
            var directoryPath = Path.Combine(basePath, folderName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fullFilePath = Path.Combine(directoryPath, newFileName);
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{schema}://{host}/{folderName}/{newFileName}";
            var imageUploadResultDto = new ImageUploadResultDto(url);

            return GeneralResult<ImageUploadResultDto>.SuccessResult(imageUploadResultDto);
        }

        public GeneralResult<bool> RemoveFile(string imageUrl, string basePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imageUrl))
                {
                    return GeneralResult<bool>.FailResult("Image URL cannot be empty");
                }

                var fileName = Path.GetFileName(imageUrl);
                var fullFilePath = Path.Combine(basePath, "Files", fileName);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                    return GeneralResult<bool>.SuccessResult(true);
                }

                return GeneralResult<bool>.FailResult("File not found on server disk");
            }
            catch (Exception ex)
            {
                return GeneralResult<bool>.FailResult($"Deletion failed: {ex.Message}");
            }
        }
    }
}