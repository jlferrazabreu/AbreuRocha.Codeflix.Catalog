﻿using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInput : IRequest<CategoryModelOutput>
{
    public GetCategoryInput(Guid id)
        => Id = id;

    public Guid Id { get; set; }
}
