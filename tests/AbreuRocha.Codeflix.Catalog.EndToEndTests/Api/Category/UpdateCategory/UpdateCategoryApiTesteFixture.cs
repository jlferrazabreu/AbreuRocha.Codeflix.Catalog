using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using AbreuRocha.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.EndToEndTests.Api.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryApiTestFixture))]
public class UpdateCategoryApiTestFixtureCollection
    : ICollectionFixture<UpdateCategoryApiTestFixture>
{ }
public class UpdateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public UpdateCategoryInput GetExampleInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );
}
