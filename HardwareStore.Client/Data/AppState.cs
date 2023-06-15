using HardwareStore.Domain.Models;

namespace HardwareStore.Client.Data;

public static class AppState
{
    public static bool IsLogged = false;
    public static User? AuthorizedUser { get; set; }
}