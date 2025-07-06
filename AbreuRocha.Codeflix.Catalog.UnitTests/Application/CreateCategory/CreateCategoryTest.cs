using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.Domain.Entity;
using AbreuRocha.Codeflix.Catalog.Domain.Exceptions;
using AbreuRocha.Codeflix.Catalog.UnitTests.Domain.Entity.Category;
using FluentAssertions;
using Moq;
using Xunit;
using UsesCases = AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;


namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new UsesCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
         );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
         );

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyName()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new UsesCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(
            _fixture.GetValidCategoryName()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
         );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
         );

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyNameAndDescription()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new UsesCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
         );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
         );

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Theory(DisplayName = nameof(ThrowWenCantInstantiateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 24,
        MemberType = typeof(CreateCategoryTestDataGenerator)
        )]
    public async void ThrowWenCantInstantiateCategory(
        CreateCategoryInput input,
        string excepitionMessage
        )
    {

        var useCase = new UsesCases.CreateCategory(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);
        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(excepitionMessage);
    }
}
