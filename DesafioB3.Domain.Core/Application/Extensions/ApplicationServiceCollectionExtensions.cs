using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioB3.Core.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}