using AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;
[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection
    : ICollectionFixture<GetCategoryTestFixture>
{ }
public class GetCategoryTestFixture
    : CategoryUseCasesBaseFixture
{
}
