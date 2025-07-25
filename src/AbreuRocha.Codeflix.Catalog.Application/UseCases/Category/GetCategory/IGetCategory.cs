using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{}
