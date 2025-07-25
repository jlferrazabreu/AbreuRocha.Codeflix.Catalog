﻿using Bogus;
using Xunit;
using FluentAssertions;
using AbreuRocha.Codeflix.Catalog.Domain.Validation;
using AbreuRocha.Codeflix.Catalog.Domain.Exceptions;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();


    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain","DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNull(value, "Value");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThorwWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThorwWhenNull()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ","");

        Action action =
            () => DomainValidation.NotNull(value, fieldName);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyTrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]

    public void NotNullOrEmptyTrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = 
            () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }

    [Fact(DisplayName =nameof(NotNullOrEmptyOK))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOK()
    {
        var target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName =nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMin),parameters: 10)]
    public void MinLengthThrowWhenLess(string? target, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
        () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be at least {minLength} characters long");
    }

    [Theory(DisplayName = nameof(MinLengthOK))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
    public void MinLengthOK(string? target, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
        () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
    public void MaxLengthOk(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesLessThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length + (new Random()).Next(0, 5);
            yield return new object[] { example, maxLength };
        }
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length - (new Random()).Next(1, 5);
            yield return new object[] { example, maxLength };
        }
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length - (new Random()).Next(1, 5);
            yield return new object[] { example, minLength };
        }
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests)
    {
        yield return new object[] {"123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length + (new Random()).Next(1, 20);
            yield return new object[] { example, minLength };
        } 
    }
}
