using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
public interface IUpdateCategory : IRequestHandler
    <UpdateCategoryInput, CategoryModelOutput>
{}
