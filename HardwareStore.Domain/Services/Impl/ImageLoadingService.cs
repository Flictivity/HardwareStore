using HardwareStore.Domain.FileSystem;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class ImageLoadingService : IImageLoadingService
{
    private static readonly string[] AcceptedFileExtensions = { ".jpg", ".png", ".jpeg" };
    private const string DefaultDisplayImageName = "default.jpg";

    private readonly IFileStorage _fileStorage;

    public ImageLoadingService(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<string> UploadImageAsync(string filename, Stream file)
    {
        if (!AcceptedFileExtensions.Contains(Path.GetExtension(filename)))
            return string.Empty;
        return await _fileStorage.UploadFile(filename, file);
    }

    public async Task<byte[]> GetImageAsBytes(string? imageId)
    {
        imageId ??= DefaultDisplayImageName;
        return await _fileStorage.DownloadFileAsBytes(imageId);
    }

    public async Task<BaseResult> DeleteImage(string id)
    {
        return await _fileStorage.DeleteFile(id);
    }
}