using AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.DeteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class  DeleteCategoryTestFixtureCollection 
    : ICollectionFixture<DeleteCategoryTestFixture>
{}
public class DeleteCategoryTestFixture : CategoryUseCaseBaseFixture
{
    
}
