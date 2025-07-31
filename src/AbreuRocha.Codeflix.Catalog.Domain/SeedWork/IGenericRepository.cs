namespace AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
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
