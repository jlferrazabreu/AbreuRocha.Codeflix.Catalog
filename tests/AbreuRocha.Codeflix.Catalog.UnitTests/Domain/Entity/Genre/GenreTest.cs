using AbreuRocha.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;
using DomainEntity = AbreuRocha.Codeflix.Catalog.Domain.Entity;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Domain.Entity.Genre;
[Collection(nameof(GenreTestFixture))]
public class GenreTest
{
    private readonly GenreTestFixture _fixture;
    public GenreTest(GenreTestFixture fixture) 
        => _fixture = fixture;
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Genre - Aggregates")]
    public void Instantiate()
    {
        var GenreName = _fixture.GetValidGenreName();
        var datetimeBefore = DateTime.Now;

        var genre = new DomainEntity.Genre(GenreName);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.Name.Should().Be(GenreName);
        genre.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (genre.CreatedAt > datetimeBefore).Should().BeTrue();
        (genre.CreatedAt < datetimeAfter).Should().BeTrue();
        (genre.IsActive).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Genre - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var datetimeBefore = DateTime.Now;
        var genre = _fixture.GetValidGenre(isActive); ;
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        genre.Should().NotBeNull();
        genre.Name.Should().Be(genre.Name);
        genre.Id.Should().NotBeEmpty();
        genre.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (genre.CreatedAt > datetimeBefore).Should().BeTrue();
        (genre.CreatedAt < datetimeAfter).Should().BeTrue();
        (genre.IsActive).Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Genre - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]

    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        Action action =
            () => new DomainEntity.Genre(name!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(Activate))]
    [Trait("Domain", "Genre - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void Activate(bool isActive)
    {
        var genre = _fixture.GetValidGenre(isActive);

        genre.Activate();

        (genre.IsActive).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Genre - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void Deactivate(bool isActive)
    {
        var genre = _fixture.GetValidGenre(isActive);

        genre.Deactivate();

        (genre.IsActive).Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Genre - Aggregates")]
    public void Update()
    {
        var genre = _fixture.GetValidGenre(true);
        var newGenreName = _fixture.GetValidGenreName();

        genre.Update(newGenreName);

        genre.Name.Should().Be(newGenreName);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Genre - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]

    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var genre = _fixture.GetValidGenre(true);
        var newGenreName = _fixture.GetValidGenreName(); 

        Action action =
            () => new DomainEntity.Genre(name!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(AddCategory))]
    [Trait("Domain", "Genre - Aggregates")]
    public void AddCategory()
    {
        var genre = _fixture.GetValidGenre();
        var categoryGuid = Guid.NewGuid();

        genre.AddCategory(categoryGuid);

        genre.Categories.Should().HaveCount(1);
        genre.Categories.Should().Contain(categoryGuid);
    }

    [Fact(DisplayName = nameof(AddTwoCategories))]
    [Trait("Domain", "Genre - Aggregates")]
    public void AddTwoCategories()
    {
        var genre = _fixture.GetValidGenre();
        var categoryGuid1 = Guid.NewGuid();
        var categoryGuid2 = Guid.NewGuid();

        genre.AddCategory(categoryGuid1);
        genre.AddCategory(categoryGuid2);

        genre.Categories.Should().HaveCount(2);
        genre.Categories.Should().Contain(categoryGuid1);
        genre.Categories.Should().Contain(categoryGuid2);
    }

    [Fact(DisplayName = nameof(RemoveCategory))]
    [Trait("Domain", "Genre - Aggregates")]
    public void RemoveCategory()
    {
        var exampleGuid = Guid.NewGuid();
        var genre = _fixture.GetValidGenre(categoriesIdsList : new List<Guid>()
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            exampleGuid,
            Guid.NewGuid(),
            Guid.NewGuid()
        });

        genre.RemoveCategory(exampleGuid);

        genre.Categories.Should().HaveCount(4);
        genre.Categories.Should().NotContain(exampleGuid);
    }

    [Fact(DisplayName = nameof(RemoveAllCategories))]
    [Trait("Domain", "Genre - Aggregates")]
    public void RemoveAllCategories()
    {
        var genre = _fixture.GetValidGenre(categoriesIdsList: new List<Guid>()
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        });

        genre.RemoveAllCategories();

        genre.Categories.Should().HaveCount(0);
    }
}
