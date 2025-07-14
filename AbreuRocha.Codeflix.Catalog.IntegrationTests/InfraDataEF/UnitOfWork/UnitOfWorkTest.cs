using UnitOfWorkInfra = AbreuRocha.Codeflix.Catalog.Infra.Data.EF;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace AbreuRocha.Codeflix.Catalog.IntegrationTests.InfraDataEF.UnitOfWork;
[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;
    public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Commit))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Commit()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategoryList = _fixture.GetExampleCategoriesList();
        await dbContext.AddRangeAsync(exampleCategoryList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var savedCategories = assertDbContext.Categories
            .AsNoTracking()
            .ToList();
        savedCategories
            .Should()
            .HaveCount(exampleCategoryList.Count);

    }

    [Fact(DisplayName = nameof(Rollback))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Rollback()
    {
        var dbContext = _fixture.CreateDbContext();
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        var task = async () => await unitOfWork.Rollback(CancellationToken.None);

        await task.Should().NotThrowAsync();
    }
}
