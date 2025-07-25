using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.EndToEndTests.Api.Category.CreateCategory;
[CollectionDefinition(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTestFixtureCollection
    : ICollectionFixture<CreateCategoryApiTestFixture>
{ }
public class CreateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public CreateCategoryInput GetExampleInput()
       => new(
           GetValidCategoryName(),
           GetValidCategoryDescription(),
           GetRandomBoolean()
       );
}
