namespace HardwareStore.Domain.Services;

public interface IEncryptionService
{
    Task<string> EncryptStringAsync(string content);
}