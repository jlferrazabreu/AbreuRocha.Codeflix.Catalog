using UseCase = AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using Xunit;
using AbreuRocha.Codeflix.Catalog.Application.Exceptions;


namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;
[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Integration/Application", "GetCategory - Use Cases")]
    public async Task GetCategory()
    {
        var dbcontext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        dbcontext.Categories.Add(exampleCategory);
        dbcontext.SaveChanges();
        var repository = new CategoryRepository(dbcontext);
        var input = new UseCase.GetCategoryInput(exampleCategory.Id);
        var useCase = new UseCase.GetCategory(repository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.Id.Should().Be(exampleCategory.Id);
        output.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoentExist))]
    [Trait("Integration/Application", "GetCategory - Use Cases")]
    public async Task NotFoundExceptionWhenCategoryDoentExist()
    {
        var dbcontext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        dbcontext.Categories.Add(exampleCategory);
        dbcontext.SaveChanges();
        var repository = new CategoryRepository(dbcontext);
        var input = new UseCase.GetCategoryInput(Guid.NewGuid());
        var useCase = new UseCase.GetCategory(repository);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<NotFoundException>().WithMessage($"Category '{input.Id}' not found.");
    }

}
