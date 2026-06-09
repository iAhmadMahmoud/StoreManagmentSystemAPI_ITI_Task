using Store.Common;

namespace Store.BLL
{
    public interface IUploadImageManager
    {
        Task<GeneralResult<ImageUploadResultDto>> UploadAsync(
             ImageUploadDto imageUploadDto,
             string basePath,
             string? schema,
             string? host);
        GeneralResult<bool> RemoveFile(string imageUrl, string basePath);
    }
}