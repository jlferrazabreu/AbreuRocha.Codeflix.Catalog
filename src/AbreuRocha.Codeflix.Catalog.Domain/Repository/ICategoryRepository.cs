using AbreuRocha.Codeflix.Catalog.Domain.Entity;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace AbreuRocha.Codeflix.Catalog.Domain.Repository;
public interface ICategoryRepository 
    : IGenericRepository<Category>,
    ISearchableRepository<Category>
{
    public Task<IReadOnlyList<Guid>> GetIdsListByIds(
        List<Guid> ids, 
        CancellationToken cancellationToken
    );
}
