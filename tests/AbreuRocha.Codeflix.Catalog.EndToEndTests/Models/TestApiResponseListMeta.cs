namespace AbreuRocha.Codeflix.Catalog.EndToEndTests.Models;

public class TestApiResponseListMeta
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }

    public TestApiResponseListMeta()
    { }
    public TestApiResponseListMeta(int currentPage, int perPage, int total)
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
    }


}
