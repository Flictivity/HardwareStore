using HardwareStore.Domain.ErrorMessages;
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

    public async Task<BaseResult> UploadImageAsync(string filename, Stream file)
    {
        if (!AcceptedFileExtensions.Contains(Path.GetExtension(filename)))
            return new BaseResult { Success = false, Message = ImageUploadErrorMessages.IncorrectExtension };
        await _fileStorage.UploadFileAsync(filename, file, true);
        return new BaseResult { Success = true };
    }

    public async Task<Stream> GetImageForExamQuestion(string? filename)
    {
        filename ??= DefaultDisplayImageName;
        return await _fileStorage.GetFileReadStreamAsync(filename);
    }
}