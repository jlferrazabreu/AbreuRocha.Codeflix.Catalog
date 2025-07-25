using  UseCase = AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using Microsoft.VisualBasic;
using Xunit;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.ListCategories;
[Collection(nameof(ListCategoriesTesteFixture))]
public class ListCategoriesTest
{
    private readonly ListCategoriesTesteFixture _fixture;
    public ListCategoriesTest(ListCategoriesTesteFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(SearchRetornsListAndTotal))]
    [Trait("Integration/Application", "ListCategories - UseCases")]
    public async Task SearchRetornsListAndTotal()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(10);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new CategoryRepository(dbContext);
        var input = new UseCase.ListCategoriesInput(1, 20);
        var useCase = new UseCase.ListCategories(categoryRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(examploCategoriesList.Count);
        output.Items.Should().HaveCount(examploCategoriesList.Count);
        foreach (CategoryModelOutput outputItem in output.Items)
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

    [Fact(DisplayName = nameof(SearchRetornsEmptyWhenEnpty))]
    [Trait("Integration/Application", "ListCategories - UseCases")]
    public async Task SearchRetornsEmptyWhenEnpty()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var categoryRepository = new CategoryRepository(dbContext);
        var input = new UseCase.ListCategoriesInput(1, 20);
        var useCase = new UseCase.ListCategories(categoryRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);
    }

    [Theory(DisplayName = nameof(SearchRetornsPaginated))]
    [Trait("Integration/Application", "ListCategories - UseCases")]
    [InlineData(10, 1, 5, 5)]
    [InlineData(10, 2, 5, 5)]
    [InlineData(7, 2, 5, 2)]
    [InlineData(7, 3, 5, 0)]
    public async Task SearchRetornsPaginated(
        int quantityCategoryToGenerate,
        int page,
        int perPage,
        int expectedQuantityItems
        )
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesList(quantityCategoryToGenerate);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new CategoryRepository(dbContext);
        var input = new UseCase.ListCategoriesInput(page, perPage);
        var useCase = new UseCase.ListCategories(categoryRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(examploCategoriesList.Count);
        output.Items.Should().HaveCount(expectedQuantityItems);
        foreach (CategoryModelOutput outputItem in output.Items)
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
    [Trait("Integration/Application", "ListCategories - UseCases")]
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
        int perPage,
        int expectedQuantityItemsRetorned,
        int expectedQuantityTotalItems
        )
    {
        var categoryNameList = new List<string>
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
        };
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var examploCategoriesList = _fixture.GetExampleCategoriesListWithNames(categoryNameList);
        await dbContext.AddRangeAsync(examploCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new CategoryRepository(dbContext);
        var input = new UseCase.ListCategoriesInput(page, perPage, search);
        var useCase = new UseCase.ListCategories(categoryRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(expectedQuantityTotalItems);
        output.Items.Should().HaveCount(expectedQuantityItemsRetorned);
        foreach (CategoryModelOutput outputItem in output.Items)
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
    [Trait("Integration/Application", "ListCategories - UseCases")]
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
        var categoryRepository = new CategoryRepository(dbContext);
        var useCaseOrder = order.ToLower() == "asc"
            ? SearchOrder.Asc
            : SearchOrder.Desc;
        var input = new UseCase.ListCategoriesInput(1, 20, "", orderBy, useCaseOrder);
        var useCase = new UseCase.ListCategories(categoryRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        var expectedOrderList = _fixture
            .CloneCategoriesListOrdered(
                examploCategoriesList, 
                input.Sort, 
                input.Dir
            );
        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(examploCategoriesList.Count);
        output.Items.Should().HaveCount(examploCategoriesList.Count);
        for (int indice = 0; indice < expectedOrderList.Count; indice++)
        {
            var outputItem = output.Items[indice];
            var exampleItem = expectedOrderList[indice];
            outputItem.Should().NotBeNull();
            exampleItem.Should().NotBeNull();
            outputItem.Id.Should().Be(exampleItem.Id);
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }
}
