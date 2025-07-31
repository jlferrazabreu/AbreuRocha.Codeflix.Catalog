namespace AbreuRocha.Codeflix.Catalog.EndToEndTests.Models;

public class TestApiResponseList<TOutputItem>
    : TestApiResponse<List<TOutputItem>>
{
    public TestApiResponseListMeta? Meta { get; set; }

    public TestApiResponseList(List<TOutputItem> data) : base(data) { }

    public TestApiResponseList()
    { }

    public TestApiResponseList(
        List<TOutputItem> data,
        TestApiResponseListMeta meta)
      : base(data)
        => Meta = meta;

    
}
