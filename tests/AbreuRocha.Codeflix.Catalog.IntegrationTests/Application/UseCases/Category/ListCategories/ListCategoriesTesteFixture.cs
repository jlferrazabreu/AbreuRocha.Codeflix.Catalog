using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Common;
using Xunit;
using DomainEntity = AbreuRocha.Codeflix.Catalog.Domain.Entity;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.ListCategories;
[CollectionDefinition(nameof(ListCategoriesTesteFixture))]
public class ListCategoriesTestFixtureCollection
    : ICollectionFixture<ListCategoriesTesteFixture>
{ }

public class ListCategoriesTesteFixture
    : CategoryUseCasesBaseFixture
{
    public List<DomainEntity.Category> GetExampleCategoriesListWithNames(List<string> names)
        => names.Select(name =>
        {
            var category = GetExampleCategory();
            category.Update(name);
            return category;
        }).ToList();

    public List<DomainEntity.Category> CloneCategoriesListOrdered(
        List<DomainEntity.Category> categoriesList,
        string orderBy,
        SearchOrder order)
    {
        var listClone = new List<DomainEntity.Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedEnumerable.ToList();
    }
}
