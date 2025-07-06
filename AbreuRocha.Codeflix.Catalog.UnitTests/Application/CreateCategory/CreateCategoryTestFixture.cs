using AbreuRocha.Codeflix.Catalog.Application.Interfaces;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.Domain.Repository;
using AbreuRocha.Codeflix.Catalog.UnitTests.Domain;
using Moq;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{
}

public class CreateCategoryTestFixture : BaseFixture
{
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription =
            Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription =
                categoryDescription[..10_000];
        return categoryDescription;
    }

    public  bool GetRandomBoolean()
        => (new Random()).NextDouble() < 0.5;

    public CreateCategoryInput GetInput()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public CreateCategoryInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetInput();
        invalidInputTooLongName.Name =
            Faker.Commerce.ProductName();
        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name +=
                Faker.Commerce.ProductName();
        return invalidInputTooLongName;
    }

    public CreateCategoryInput GetInvalidInputDescriptionNull()
    {
        var invalidInputDescriptionNull = GetInput();
        invalidInputDescriptionNull.Description = null!;
        return invalidInputDescriptionNull;
    }

    public CreateCategoryInput GetInvalidInputTooLongDescription()
    {
        var invalidInputTooLongDescription = GetInput();
        invalidInputTooLongDescription.Description =
            Faker.Commerce.ProductDescription();
        while (invalidInputTooLongDescription.Description.Length <= 10_000)
            invalidInputTooLongDescription.Description +=
                Faker.Commerce.ProductDescription();
        return invalidInputTooLongDescription;
    }

    public Mock<ICategoryRepository> GetRepositoryMock()
        => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();
}
