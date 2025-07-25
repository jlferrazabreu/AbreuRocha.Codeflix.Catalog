using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection 
    : ICollectionFixture<UpdateCategoryTestFixture>
{}
public class UpdateCategoryTestFixture : CategoryUseCaseBaseFixture
{
    public UpdateCategoryInput GetValidInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );

    public UpdateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public UpdateCategoryInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        invalidInputTooLongName.Name =
            Faker.Commerce.ProductName();
        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name +=
                Faker.Commerce.ProductName();
        return invalidInputTooLongName;
    }

    public UpdateCategoryInput GetInvalidInputTooLongDescription()
    {
        var invalidInputTooLongDescription = GetValidInput();
        invalidInputTooLongDescription.Description =
            Faker.Commerce.ProductDescription();
        while (invalidInputTooLongDescription.Description.Length <= 10_000)
            invalidInputTooLongDescription.Description +=
                Faker.Commerce.ProductDescription();
        return invalidInputTooLongDescription;
    }
}
