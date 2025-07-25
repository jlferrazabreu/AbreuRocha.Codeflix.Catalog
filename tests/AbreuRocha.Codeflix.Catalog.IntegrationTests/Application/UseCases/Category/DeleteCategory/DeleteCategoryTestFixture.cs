using AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.DeleteCategory;
[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection
    : ICollectionFixture<DeleteCategoryTestFixture>
{ }
public class DeleteCategoryTestFixture
    : CategoryUseCasesBaseFixture
{ }
