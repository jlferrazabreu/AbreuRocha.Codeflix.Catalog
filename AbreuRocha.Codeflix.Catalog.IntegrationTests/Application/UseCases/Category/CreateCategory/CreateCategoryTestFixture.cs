using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;
[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection
    : ICollectionFixture<CreateCategoryTestFixture>
{ }
public class CreateCategoryTestFixture
    : CategoryUseCasesBaseFixture
{ 
    public CreateCategoryInput GetInput()
    {
        var category = GetExampleCategory();
        return new CreateCategoryInput(
            category.Name,
            category.Description,
            category.IsActive
        );
    }

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
}
