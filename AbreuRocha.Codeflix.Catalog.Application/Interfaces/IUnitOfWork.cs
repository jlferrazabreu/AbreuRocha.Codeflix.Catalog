namespace AbreuRocha.Codeflix.Catalog.Application.Interfaces;
public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}
