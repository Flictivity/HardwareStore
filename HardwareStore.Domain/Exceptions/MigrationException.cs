namespace HardwareStore.Domain.Exceptions;

public class MigrationException : System.Exception
{
    public MigrationException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}