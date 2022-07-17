using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Demo.Core.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _db;
    private readonly IDomainEventDispatcher _dispatcher;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DatabaseContext db, IDomainEventDispatcher dispatcher)
    {
        _db = db;
        _dispatcher = dispatcher;
    }
    
    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dispatcher.DispatchDomainEvents(_db);
            await _db.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
        }
    }

    private void RollbackTransaction()
    {
        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction?.Dispose();
        }
    }
}
