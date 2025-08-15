using AbreuRocha.Codeflix.Catalog.UnitTests.Common;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Genre.Common;
public class GenreUseCasesBaseFixture 
    : BaseFixture
{
    public string GetValidGenreName()
        => Faker.Commerce.Categories(1)[0];
}
