using HardwareStore.Domain.FileSystem;
using HardwareStore.Domain.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace HardwareStore.GridFS;

public class FileStorage : IFileStorage

{
    private readonly GridFSBucket _gridFs;

    public FileStorage(MongoConnection connection)
    {
        _gridFs = new GridFSBucket(connection.Database);
    }

    public async Task<string> UploadFile(string fileName, Stream stream)
    {
        return (await _gridFs.UploadFromStreamAsync(fileName, stream)).ToString();
    }

    public async Task<byte[]> DownloadFileAsBytes(string id)
    {
        return await _gridFs.DownloadAsBytesAsync(ObjectId.Parse(id));
    }

    public async Task<BaseResult> DeleteFile(string id)
    {
        
        await _gridFs.DeleteAsync(ObjectId.Parse(id));
        return new BaseResult {Success = true};
    }

    private async Task DeleteExistingFiles(string filename)
    {
        var existing = await _gridFs.FindAsync(Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, filename));
        while (await existing.MoveNextAsync())
        {
            var files = existing.Current;
            foreach (var existingFile in files)
            {
                await _gridFs.DeleteAsync(existingFile.Id);
            }
        }
    }
}