using AbreuRocha.Codeflix.Catalog.UnitTests.Application.Genre.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Genre.Delete;
[CollectionDefinition(nameof(DeleteGenreFixture))]
public class DeleteGenreFixtureCollection
    : ICollectionFixture<DeleteGenreFixture>
{ }
public class DeleteGenreFixture
    : GenreUseCasesBaseFixture
{ }
