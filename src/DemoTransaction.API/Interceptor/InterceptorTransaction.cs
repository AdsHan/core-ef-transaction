using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace DemoTransaction.API.Interceptor;

public class InterceptorTransaction : DbTransactionInterceptor
{

    public override DbTransaction TransactionStarted(DbConnection connection, TransactionEndEventData eventData, DbTransaction result)
    {
        return result;
    }

    public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
    {

    }

    public override ValueTask<DbTransaction> TransactionStartedAsync(DbConnection connection, TransactionEndEventData eventData, DbTransaction result, CancellationToken cancellationToken = default(CancellationToken))
    {
        return new ValueTask<DbTransaction>(result);
    }

    public override Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.CompletedTask;
    }

}