using AbreuRocha.Codeflix.Catalog.Application.Common;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
public class ListCategoriesOutput
    : PaginateListOutput<CategoryModelOutput>
{
    public ListCategoriesOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<CategoryModelOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}
