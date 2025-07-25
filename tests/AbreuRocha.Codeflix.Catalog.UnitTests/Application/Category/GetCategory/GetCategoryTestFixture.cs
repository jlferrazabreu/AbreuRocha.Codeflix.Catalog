using AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection :
    ICollectionFixture<GetCategoryTestFixture>
{ }
public class GetCategoryTestFixture : CategoryUseCaseBaseFixture
{
}
