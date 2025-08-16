using AbreuRocha.Codeflix.Catalog.Application.Interfaces;
using AbreuRocha.Codeflix.Catalog.Domain.Repository;
using AbreuRocha.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = AbreuRocha.Codeflix.Catalog.Domain.Entity;
using Moq;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Genre.Common;
public class GenreUseCasesBaseFixture 
    : BaseFixture
{
    public Mock<IGenreRepository> GetGenreRepositoryMock()
        => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();

    public Mock<ICategoryRepository> GetCategoryRepositoryMock()
        => new();
    public List<Guid> GetRandomIdsList(int? count = null)
        => Enumerable
            .Range(1, count ?? (new Random()).Next(1, 10))
            .Select(_ => Guid.NewGuid())
            .ToList();

    public DomainEntity.Genre GetExamploGenre(
        bool? isctive = null,
        List<Guid>? categoriesIds = null)
    {
        var genre = new DomainEntity.Genre(
                GetValidGenreName(),
                isctive ?? GetRandomBoolean()
            );
        categoriesIds?.ForEach(genre.AddCategory);
        return genre;
    }

    public string GetValidGenreName()
        => Faker.Commerce.Categories(1)[0];
}
