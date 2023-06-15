using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Services;
using HardwareStore.Domain.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareStore.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        return services;
    }
}