using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Repository = AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.InfraDataEF.Repositories.CategoryRepository;

[Collection(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTest
{
    private readonly CategoryRepositoryTestFixture _fixture;

    public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Insert))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Insert()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategory = _fixture.GetExampleCategory();
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        await categoryRepository.Insert(examploCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbCategory = await dbContext.Categories.FindAsync(examploCategory.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(examploCategory.Name);
        dbCategory.Description.Should().Be(examploCategory.Description);
        dbCategory.IsActive.Should().Be(examploCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(examploCategory.CreatedAt);
    }
}
