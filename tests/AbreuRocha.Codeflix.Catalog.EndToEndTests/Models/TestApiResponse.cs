namespace AbreuRocha.Codeflix.Catalog.EndToEndTests.Models;
public class TestApiResponse<TOutput>
{
    public TOutput? Data { get; set; }
    public TestApiResponse(TOutput data)
    {
        Data = data;
    }
    public TestApiResponse()
    { }
}


