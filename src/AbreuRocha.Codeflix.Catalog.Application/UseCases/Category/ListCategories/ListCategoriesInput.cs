using AbreuRocha.Codeflix.Catalog.Application.Common;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
public class ListCategoriesInput
    : PaginateListInput,
    IRequest<ListCategoriesOutput>
{
    public ListCategoriesInput(
        int page = 1, 
        int perPage = 15, 
        string search = "", 
        string sort = "", 
        SearchOrder dir = SearchOrder.Asc) 
        : base(page, perPage, search, sort, dir)
    {
    }
}
