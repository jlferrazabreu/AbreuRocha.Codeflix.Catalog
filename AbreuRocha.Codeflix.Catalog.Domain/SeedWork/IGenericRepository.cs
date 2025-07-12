using AbreuRocha.Codeflix.Catalog.Domain.Entity;

namespace AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert(
        TAggregate aggergate,
        CancellationToken cancellationToken
    );

    public Task<TAggregate> Get(
        Guid Id,
        CancellationToken cancellationToken
    );

    public Task Delete(
        TAggregate aggergate,
        CancellationToken cancellationToken
    );

    public Task Update(
        TAggregate aggergate,
        CancellationToken cancellationToken
    );
}
