using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace AbreuRocha.Codeflix.Catalog.Api.Configurations;

public static class ConectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services)
    {
        services.AddDbConection();
        return services;
    }

    private static IServiceCollection AddDbConection(
        this IServiceCollection services)
    {
        services.AddDbContext<CodeflixCatalogDbContext>(
            options => options.UseInMemoryDatabase("InMemory-DSV-Database")
            );
        return services;
    }
}
