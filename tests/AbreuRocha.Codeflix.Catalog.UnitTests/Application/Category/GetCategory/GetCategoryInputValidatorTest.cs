﻿using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;
[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputValidatorTest
{
    private readonly GetCategoryTestFixture _fixture;
    public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetCategoryInputValidationTest - UseCases")]
    public void ValidationOk()
    {
        var ValidInput = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidator();

        var validationResult = validator.Validate(ValidInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(InvalidWhenEmptyGuid))]
    [Trait("Application", "GetCategoryInputValidationTest - UseCases")]
    public void InvalidWhenEmptyGuid()
    {
        var invalidInput = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidator();

        var validationResult = validator.Validate(invalidInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }
}
