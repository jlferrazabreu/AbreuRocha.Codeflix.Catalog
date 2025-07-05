using AbreuRocha.Codeflix.Catalog.Domain.Entity;

namespace AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert(
        TAggregate aggergate,
        CancellationToken cancellationToken = default
    );
}
