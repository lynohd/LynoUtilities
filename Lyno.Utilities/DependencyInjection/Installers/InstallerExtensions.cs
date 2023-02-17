using Lyno.Utilities.DependencyInjection.Installers.Attributes;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Reflection;

namespace Lyno.Utilities.DependencyInjection.Installers;
public static class InstallerExtensions
{
    static ILogger Logger;
    static InstallerExtensions()
    {
        Logger = LoggerFactory.Create(x =>
        {
            x.SetMinimumLevel(LogLevel.Debug);
            x.AddConsole();
        }).CreateLogger("ServiceInstaller");
    }

    public static void Install(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies) => assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(IsValidInstaller)
            .Where(HasAttribute<InstallerAttribute>)
            .Where(IsEnabled)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .ToList()
            .ForEach(x => x.Install(services, config));


    static bool IsValidInstaller(TypeInfo typeInfo)
    {
        return typeInfo.IsAssignableTo(typeof(IServiceInstaller)) == true && typeInfo.IsInterface == false;
    }

    static bool HasAttribute<TAttribute>(TypeInfo typeInfo) where TAttribute : Attribute
    {
        var hasAttribute = typeInfo.GetCustomAttribute<TAttribute>() != null;

        if(hasAttribute is false)
        {
            Logger.LogWarning("{Service} does not have {Attribute} and cannot be resolved", typeInfo, nameof(InstallerAttribute));
        }

        return hasAttribute;
    }

    static bool IsEnabled(TypeInfo info)
    {
        var enabled = info.GetCustomAttribute<InstallerAttribute>().Enabled;
        Logger.LogInformation("{Service} has been registered", info);
        return enabled;
    }
}
