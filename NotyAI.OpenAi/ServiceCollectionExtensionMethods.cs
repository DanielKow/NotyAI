using Microsoft.Extensions.DependencyInjection;

namespace NotyAI.OpenAI;

public static class ServiceCollectionExtensionMethods
{
    public static IServiceCollection AddOpenAiService(this IServiceCollection services)
    {
        services.AddTransient<IOpenAiService, OpenAiService>();
        return services;
    }  
}