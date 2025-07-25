﻿using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Entity = AbreuRocha.Codeflix.Catalog.Domain.Entity;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace AbreuRocha.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;
[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
{}

public class ListCategoriesTestFixture
    :CategoryUseCaseBaseFixture
{
    public List<Entity.Category> GetExampleCategoriesList(int length = 10)
    {
        var list = new List<Entity.Category>();
        for (int i = 0; i < length; i++)
            list.Add(GetExampleCategory());

        return list;
    }

    public ListCategoriesInput GetExampleInput()
    {
        var random = new Random();
        return new ListCategoriesInput(
            page: random.Next(1,10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0,10) > 5 ? 
                SearchOrder.Asc : SearchOrder.Desc
        );
    }
}
