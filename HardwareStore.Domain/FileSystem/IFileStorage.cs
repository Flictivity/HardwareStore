using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.FileSystem;

public interface IFileStorage
{
    Task<string> UploadFile(string fileName, Stream stream);
    Task<byte[]> DownloadFileAsBytes(string id);
    Task<BaseResult> DeleteFile(string id);
}