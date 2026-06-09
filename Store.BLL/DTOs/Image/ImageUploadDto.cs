using Microsoft.AspNetCore.Http;

namespace Store.BLL
{
    public sealed record ImageUploadDto(IFormFile File);
}
