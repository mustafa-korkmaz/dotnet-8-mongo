namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task UseTransactionAsync(Func<Task> transactionBody);

        /// <summary>
        /// Returns document repository inherited from  IRepository
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>() where TRepository : class;
    }
}