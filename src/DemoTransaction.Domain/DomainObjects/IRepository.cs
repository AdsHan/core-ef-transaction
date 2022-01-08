namespace DemoTransaction.Domain.DomainObjects;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    Task SaveAsync();
    void Add(T obj);
}
