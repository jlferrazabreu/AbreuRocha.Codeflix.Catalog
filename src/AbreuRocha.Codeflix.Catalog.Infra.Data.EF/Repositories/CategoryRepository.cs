using AbreuRocha.Codeflix.Catalog.Application.Exceptions;
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

    public async Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(
                x => x.Id == Id,
                cancellationToken
                );
        NotFoundException
            .ThrowIfNull(category, $"Category '{Id}' not found.");
        return category!;
    }

    public Task Update(Category aggergate, CancellationToken _)
        => Task.FromResult( _categories.Update(aggergate));

    public Task Delete(Category aggergate, CancellationToken _)
        => Task.FromResult(_categories.Remove(aggergate));



    public async Task<SearchOutput<Category>> Search(
        SearchInput input,
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!string.IsNullOrWhiteSpace(input.Search))
        {
            query = query.Where(x => x.Name.Contains(input.Search));
        }
        var total = await query.CountAsync();
        var items = await query
            .AsNoTracking()
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();

        return new SearchOutput<Category>(
            input.Page,
            input.PerPage,
            total,
            items
        );
    }

    private IQueryable<Category> AddOrderToQuery(
        IQueryable<Category> query, 
        string orderProperty, 
        SearchOrder order)
        => (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
        };
}
