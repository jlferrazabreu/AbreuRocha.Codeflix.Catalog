using AbreuRocha.Codeflix.Catalog.Application.Interfaces;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.Domain.Repository;
using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateCategory));
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddTransient<
            ICategoryRepository, CategoryRepository>();
        services.AddTransient<
            IUnitOfWork, UnitOfWork>();
        return services;
    }
}
