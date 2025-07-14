using AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.Base;
public class BaseFixture
{
    public BaseFixture()
        => Faker = new Faker("pt_BR");

    protected Faker Faker { get; set; }

    public CodeflixCatalogDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new CodeflixCatalogDbContext(
                new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                    .UseInMemoryDatabase("integration-tests-db")
                    .Options
            );
        if (!preserveData)
            context.Database.EnsureDeleted();
        return context;
    }
}
