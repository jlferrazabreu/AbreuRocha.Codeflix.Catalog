using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
public interface IListCategories
    : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
{}
