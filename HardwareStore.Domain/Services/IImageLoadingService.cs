using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface IImageLoadingService
{
    public Task<string> UploadImageAsync(string filename, Stream file);
    public Task<byte[]> GetImageAsBytes(string? imageId);
    public Task<BaseResult> DeleteImage(string id);
}