using AbreuRocha.Codeflix.Catalog.Application.Exceptions;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.Common;
using FluentAssertions;
using Moq;
using Xunit;
using DomainEntity = AbreuRocha.Codeflix.Catalog.Domain.Entity;
using UseCase = AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.DeleteGenre;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Genre.Delete;
[Collection(nameof(DeleteGenreFixture))]
public class DeleteGenreTest
{
    private readonly DeleteGenreFixture _fixture;

    public DeleteGenreTest(DeleteGenreFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteGenre))]
    [Trait("Application", "DeleteGenre - Use Cases")]
    public async void DeleteGenre()
    {
        var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var exampleGenre = _fixture.GetExamploGenre();
        genreRepositoryMock.Setup(x => x.Get(
            It.Is<Guid>(x => x == exampleGenre.Id),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleGenre);
        var useCase = new UseCase.DeleteGenre(
            genreRepositoryMock.Object,
            unitOfWorkMock.Object
        );              
        var input = new UseCase.DeleteGenreInput(
            exampleGenre.Id
        );

        await useCase.Handle(input, CancellationToken.None);

        genreRepositoryMock.Verify(x => x.Get(
            It.Is<Guid>(x => x == exampleGenre.Id),
            It.IsAny<CancellationToken>()
        ), Times.Once);
        genreRepositoryMock.Verify(x => x.Delete(
                It.Is<DomainEntity.Genre>(x => x.Id == exampleGenre.Id),
                It.IsAny<CancellationToken>()
        ), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact(DisplayName = nameof(ThrowWhenNotFound))]
    [Trait("Application", "DeleteGenre - Use Cases")]
    public async void ThrowWhenNotFound()
    {
        var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var exampleId = Guid.NewGuid();

        genreRepositoryMock.Setup(x => x.Get(
            It.Is<Guid>(x => x == exampleId),
            It.IsAny<CancellationToken>()
            )).ThrowsAsync(new NotFoundException(
                $"Genre '{exampleId}' not found")
            );
        var useCase = new UseCase.DeleteGenre(
            genreRepositoryMock.Object,
            unitOfWorkMock.Object
        );
        var input = new UseCase.DeleteGenreInput(
            exampleId
        );

        var action = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Genre '{exampleId}' not found");
        genreRepositoryMock.Verify(
            x => x.Get(
                It.Is<Guid>(x => x == exampleId),
                It.IsAny<CancellationToken>()
            ), Times.Once);

    }
}
