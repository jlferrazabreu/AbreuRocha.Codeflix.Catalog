using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Common;
using Xunit;
using Entity = AbreuRocha.Codeflix.Catalog.Domain.Entity;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.ListCategories;
[CollectionDefinition(nameof(ListCategoriesTesteFixture))]
public class ListCategoriesTestFixtureCollection
    : ICollectionFixture<ListCategoriesTesteFixture>
{ }

public class ListCategoriesTesteFixture
    : CategoryUseCasesBaseFixture
{
    public List<Entity.Category> GetExampleCategoriesListWithNames(List<string> names)
        => names.Select(name =>
        {
            var category = GetExampleCategory();
            category.Update(name);
            return category;
        }).ToList();

    public List<Entity.Category> CloneCategoriesListOrdered(
        List<Entity.Category> categoriesList,
        string orderBy,
        SearchOrder order)
    {
        var listClone = new List<Entity.Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name)
        };
        return orderedEnumerable.ToList();
    }
}
