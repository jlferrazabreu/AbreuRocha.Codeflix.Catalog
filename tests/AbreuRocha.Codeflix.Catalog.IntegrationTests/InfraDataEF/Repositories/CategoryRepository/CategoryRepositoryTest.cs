using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Repository = AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using AbreuRocha.Codeflix.Catalog.Application.Exceptions;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using AbreuRocha.Codeflix.Catalog.Domain.Entity;

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

        var dbCategory = await (_fixture.CreateDbContext(true))
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
            _fixture.CreateDbContext(true));

       

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
            _fixture.CreateDbContext(true));

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

        var dbCategory = await (_fixture.CreateDbContext(true))
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

        var dbCategory = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(examploCategory.Id);
        dbCategory.Should().BeNull();
       
    }

    [Fact(DisplayName = nameof(SearchRetornsListAndTotal))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task SearchRetornsListAndTotal()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(15);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchinput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

       var output = await categoryRepository.Search(
            searchinput,
            CancellationToken.None);
        await dbContext.SaveChangesAsync();

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchinput.Page);
        output.PerPage.Should().Be(searchinput.PerPage);
        output.Total.Should().Be(examploCategoriesList.Count);
        output.Items.Should().HaveCount(examploCategoriesList.Count);
        foreach (Category outputItem in output.Items)
        {
            var exampleItem = examploCategoriesList
                .Find(x => x.Id == outputItem.Id);
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }

    [Fact(DisplayName = nameof(SearchRetornsEmptyPersistenceIsEmpty))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task SearchRetornsEmptyPersistenceIsEmpty()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchinput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

        var output = await categoryRepository.Search(
             searchinput,
             CancellationToken.None);
        await dbContext.SaveChangesAsync();

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchinput.Page);
        output.PerPage.Should().Be(searchinput.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);
    }

    [Theory(DisplayName = nameof(SearchRetornsPaginated))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    [InlineData(10, 1, 5, 5)]
    [InlineData(10, 2, 5, 5)]
    [InlineData(7, 2, 5, 2)]
    [InlineData(7, 3, 5, 0)]
    public async Task SearchRetornsPaginated(
        int quantityCategoryToGenerate,
        int page,
        int perpage,
        int expectedQuantityItems
        )
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(quantityCategoryToGenerate);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchinput = new SearchInput(page, perpage, "", "", SearchOrder.Asc);

        var output = await categoryRepository.Search(
             searchinput,
             CancellationToken.None);
        await dbContext.SaveChangesAsync();

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchinput.Page);
        output.PerPage.Should().Be(searchinput.PerPage);
        output.Total.Should().Be(quantityCategoryToGenerate);
        output.Items.Should().HaveCount(expectedQuantityItems);
        foreach (Category outputItem in output.Items)
        {
            var exampleItem = examploCategoriesList
                .Find(x => x.Id == outputItem.Id);
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }

    [Theory(DisplayName = nameof(SearchByText))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    [InlineData("Action", 1, 5, 1, 1)]
    [InlineData("Horror", 1, 5, 3, 3)]
    [InlineData("Horror", 2, 5, 0, 3)]
    [InlineData("Sci-fi", 1, 5, 4, 4)]
    [InlineData("Sci-fi", 1, 2, 2, 4)]
    [InlineData("Sci-fi", 2, 3, 1, 4)]
    [InlineData("Sci-fi Other", 1, 3, 0, 0)]
    [InlineData("Robots", 1, 5, 2, 2)]
    public async Task SearchByText(
        string search,
        int page,
        int perpage,
        int expectedQuantityItemsRetorned,
        int expectedQuantityTotalItems
        )
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesListWithNames(new List<string>()
        {
            "Action",
            "Horror",
            "Horror - Robots",
            "Horror - Based on Real Facts",
            "Drama",
            "Sci-fi IA",
            "Sci-fi Space",
            "Sci-fi Robots",
            "Sci-fi Future",
        });
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchinput = new SearchInput(page, perpage, search, "", SearchOrder.Asc);

        var output = await categoryRepository.Search(
             searchinput,
             CancellationToken.None);
        await dbContext.SaveChangesAsync();

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchinput.Page);
        output.PerPage.Should().Be(searchinput.PerPage);
        output.Total.Should().Be(expectedQuantityTotalItems);
        output.Items.Should().HaveCount(expectedQuantityItemsRetorned);
        foreach (Category outputItem in output.Items)
        {
            var exampleItem = examploCategoriesList
                .Find(x => x.Id == outputItem.Id);
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }

    [Theory(DisplayName = nameof(SearchOrdered))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    [InlineData("name", "asc")]
    [InlineData("name", "desc")]
    [InlineData("id", "asc")]
    [InlineData("id", "desc")]
    [InlineData("createdAt", "asc")]
    [InlineData("createdAt", "desc")]
    [InlineData("", "asc")]
    public async Task SearchOrdered(
        string orderBy,
        string order 
        )
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(10);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchOrder = order.ToLower() == "asc" 
            ? SearchOrder.Asc 
            : SearchOrder.Desc;
        var searchInput = new SearchInput(1, 20, "", orderBy, searchOrder);

        var output = await categoryRepository.Search(
             searchInput,
             CancellationToken.None);

        var expectedOrderList = _fixture.CloneCategoriesListOrdered(
            examploCategoriesList,
            orderBy,
            searchOrder);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(examploCategoriesList.Count);
        output.Items.Should().HaveCount(examploCategoriesList.Count);
        for(int indice = 0; indice < expectedOrderList.Count; indice++)
        {
            var expectedItem = expectedOrderList[indice];
            var outputItem = output.Items[indice];
            expectedItem.Should().NotBeNull();
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(expectedItem!.Name);
            outputItem.Id.Should().Be(expectedItem.Id);
            outputItem.Description.Should().Be(expectedItem.Description);
            outputItem.IsActive.Should().Be(expectedItem.IsActive);
            outputItem.CreatedAt.Should().Be(expectedItem.CreatedAt);
        }
    }
}
