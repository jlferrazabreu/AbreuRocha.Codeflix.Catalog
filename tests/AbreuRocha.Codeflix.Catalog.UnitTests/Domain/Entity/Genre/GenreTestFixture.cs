using AbreuRocha.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = AbreuRocha.Codeflix.Catalog.Domain.Entity;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Domain.Entity.Genre;
[CollectionDefinition(nameof(GenreTestFixture))]
public class GenreTestFixtureCollection
    : ICollectionFixture<GenreTestFixture>
{ }
public class GenreTestFixture
    : BaseFixture
{
    public GenreTestFixture()
    { }

    public string GetValidGenreName()
    {
        var genreName = "";
        while (genreName.Length == 0)
            genreName = Faker.Commerce.Categories(1)[0];
        return genreName;
    }

    public DomainEntity.Genre GetValidGenre(
        bool isActive = true,
        List<Guid>? categoriesIdsList = null
    )
    {
        var genre =  new DomainEntity.Genre(GetValidGenreName(), isActive);
        if(categoriesIdsList is not null)
            foreach(var categoryId in categoriesIdsList)
                genre.AddCategory(categoryId);
        return genre;
    }
}

