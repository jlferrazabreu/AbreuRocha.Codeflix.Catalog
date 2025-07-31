using AbreuRocha.Codeflix.Catalog.Api.Extensions.String;
using System.Text.Json;

namespace AbreuRocha.Codeflix.Catalog.Api.Configurations.Policies;

public class JsonSnekeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) 
        => name.ToSnakeCase();
}
