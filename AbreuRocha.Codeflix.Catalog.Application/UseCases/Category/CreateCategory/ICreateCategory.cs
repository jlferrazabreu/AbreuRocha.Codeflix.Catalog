﻿namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
public interface ICreateCategory
{
    public Task<CreateCategoryOutput> Handle(
        CreateCategoryInput input,
        CancellationToken cancellationToken = default
    );
}
