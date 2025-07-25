﻿using AbreuRocha.Codeflix.Catalog.Application.Exceptions;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Entity = AbreuRocha.Codeflix.Catalog.Domain.Entity;
using AbreuRocha.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;
    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCase")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategory(
        Entity.Category exampleCategory,
        UpdateCategoryInput input)
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock
            .Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
             ).ReturnsAsync(exampleCategory);

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        CategoryModelOutput output =  await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be((bool)input.IsActive!);
        repositoryMock.Verify(
            x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>()),
            Times.Once
        );  
        repositoryMock.Verify(
            x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        unitOfWorkMock.Verify(
            x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
    }

    [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingIsActive))]
    [Trait("Application", "UpdateCategory - UseCase")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategoryWithoutProvidingIsActive(
        Entity.Category exampleCategory,
        UpdateCategoryInput exampleInput)
    {
        var input = new UpdateCategoryInput(
            exampleInput.Id,
            exampleInput.Name,
            exampleInput.Description
        );
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock
            .Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
             ).ReturnsAsync(exampleCategory);

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        repositoryMock.Verify(
            x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        repositoryMock.Verify(
            x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        unitOfWorkMock.Verify(
            x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
    }

    [Theory(DisplayName = nameof(UpdateCategoryOnlyName))]
    [Trait("Application", "UpdateCategory - UseCase")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategoryOnlyName(
        Entity.Category exampleCategory,
        UpdateCategoryInput exampleInput)
    {
        var input = new UpdateCategoryInput(
            exampleInput.Id,
            exampleInput.Name
        );
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock
            .Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
             ).ReturnsAsync(exampleCategory);

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        repositoryMock.Verify(
            x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        repositoryMock.Verify(
            x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        unitOfWorkMock.Verify(
            x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
    }

    [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
    [Trait("Application", "UpdateCategory - UseCase")]
    public async Task ThrowWhenCategoryNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var input = _fixture.GetValidInput();

        repositoryMock
            .Setup(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
             ).ThrowsAsync(
                new NotFoundException($"Category {input.Id} not found"));

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);
        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(
            x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Theory(DisplayName = nameof(ThrowWhenCantUpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCase")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 12,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ThrowWhenCantUpdateCategory(UpdateCategoryInput input, string expectedExceptionMessage)
    {
        var exampleCategory = _fixture.GetExampleCategory();
        input.Id = exampleCategory.Id;
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        repositoryMock.Setup(
           x => x.Get(
              exampleCategory.Id,
               It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );
        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);
        
        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedExceptionMessage);

        repositoryMock.Verify(x => x.Get(
            exampleCategory.Id,
            It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
