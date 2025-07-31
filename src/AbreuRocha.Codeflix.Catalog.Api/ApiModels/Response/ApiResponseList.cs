using AbreuRocha.Codeflix.Catalog.Application.Common;

namespace AbreuRocha.Codeflix.Catalog.Api.ApiModels.Response;

public class ApiResponseList<TItemData> 
    : ApiResponse<IReadOnlyList<TItemData>>
{
    public ApiResponseListMeta Meta { get; private set; }
    public ApiResponseList(
        int currentPage,
        int perPage,
        int total,
        IReadOnlyList<TItemData> data) 
        : base(data)
    {
        Meta = new ApiResponseListMeta(currentPage, perPage, total);
    }

    public ApiResponseList(
        PaginateListOutput<TItemData> paginateListOutput)
        : base(paginateListOutput.Items)
    {
        Meta = new ApiResponseListMeta(
            paginateListOutput.Page,
            paginateListOutput.PerPage,
            paginateListOutput.Total);
    }
}
