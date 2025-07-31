using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace AbreuRocha.Codeflix.Catalog.Api.Configurations;

public static class ConectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbConection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConection(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("CatalogDb");
        services.AddDbContext<CodeflixCatalogDbContext>(
            options => options.UseMySql(
                connectionString,
                ServerVersion
                    .AutoDetect(connectionString))
            );
        return services;
    }
}
