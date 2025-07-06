namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputslist = new List<object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputslist.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    invalidInputslist.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should be less or equal 255 characters long"
                    });
                    break;
                case 2:
                    invalidInputslist.Add(new object[] {
                        fixture.GetInvalidInputDescriptionNull(),
                        "Description should not be null"
                    });
                    break;
                case 3:
                    invalidInputslist.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description should be less or equal 10000 characters long"
                    });
                    break;
            }
        }

        return invalidInputslist;
    }
}
