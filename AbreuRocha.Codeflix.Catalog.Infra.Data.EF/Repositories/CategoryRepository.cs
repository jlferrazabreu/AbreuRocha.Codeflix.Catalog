using AbreuRocha.Codeflix.Catalog.Domain.Entity;
using AbreuRocha.Codeflix.Catalog.Domain.Repository;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace AbreuRocha.Codeflix.Catalog.Infra.Data.EF.Repositories;
public class CategoryRepository
    : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _context;
    private DbSet<Category> _categories 
        => _context.Set<Category>();

    public CategoryRepository(CodeflixCatalogDbContext context)
        => _context = context;


    public async Task Insert(
        Category aggergate, 
        CancellationToken cancellationToken
    )
    {
        await _categories.AddAsync(aggergate, cancellationToken);
    }
    public Task Delete(Category aggergate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category aggergate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
