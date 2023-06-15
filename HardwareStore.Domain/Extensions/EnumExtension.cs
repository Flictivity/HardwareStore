using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HardwareStore.Domain.Extensions;

public static class EnumExtension
{
    public static string? GetName(this Enum @enum)
    {
        return @enum.GetType().GetField(@enum.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name;
    }
}