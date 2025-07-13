using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Repository = AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using AbreuRocha.Codeflix.Catalog.Application.Exceptions;

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

        var dbCategory = await (_fixture.CreateDbContext())
            .Categories.FindAsync(examploCategory.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(examploCategory.Name);
        dbCategory.Description.Should().Be(examploCategory.Description);
        dbCategory.IsActive.Should().Be(examploCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(examploCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(Get))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Get()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategory = _fixture.GetExampleCategory();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(15);
        examploCategoriesList.Add(examploCategory);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(
            _fixture.CreateDbContext());

       

        var dbCategory = await categoryRepository.Get(
            examploCategory.Id, 
            CancellationToken.None);

        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(examploCategory.Name);
        dbCategory.Id.Should().Be(examploCategory.Id);
        dbCategory.Description.Should().Be(examploCategory.Description);
        dbCategory.IsActive.Should().Be(examploCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(examploCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(GetThrowIfNotFound))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task GetThrowIfNotFound()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleId = Guid.NewGuid();
        await dbContext.AddRangeAsync(_fixture.GetExampleCategoriesList(15));
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(
            _fixture.CreateDbContext());

        var task = async() => await categoryRepository.Get(
            exampleId,
            CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{exampleId}' not found.");
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Update()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategory = _fixture.GetExampleCategory();
        var newCategoryValues = _fixture.GetExampleCategory();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(15);
        examploCategoriesList.Add(examploCategory);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        examploCategory.Update(
            newCategoryValues.Name,
            newCategoryValues.Description
        );
        await categoryRepository.Update(
            examploCategory,
            CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext())
            .Categories.FindAsync(examploCategory.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(examploCategory.Name);
        dbCategory.Id.Should().Be(examploCategory.Id);
        dbCategory.Description.Should().Be(examploCategory.Description);
        dbCategory.IsActive.Should().Be(examploCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(examploCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(Delete))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Delete()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategory = _fixture.GetExampleCategory();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(15);
        examploCategoriesList.Add(examploCategory);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        await categoryRepository.Delete(
            examploCategory,
            CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext())
            .Categories.FindAsync(examploCategory.Id);
        dbCategory.Should().BeNull();
       
    }
}
