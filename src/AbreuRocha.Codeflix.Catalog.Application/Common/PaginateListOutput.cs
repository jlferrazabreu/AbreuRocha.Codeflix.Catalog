namespace AbreuRocha.Codeflix.Catalog.Application.Common;
public abstract class PaginateListOutput<TOutputItem>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutputItem> Items { get; set; }

    protected PaginateListOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<TOutputItem> items)
    {
        Page = page;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}
