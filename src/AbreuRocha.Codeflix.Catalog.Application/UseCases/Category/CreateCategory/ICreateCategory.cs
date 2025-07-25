using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
public interface ICreateCategory : 
    IRequestHandler<CreateCategoryInput, CategoryModelOutput>
{}
