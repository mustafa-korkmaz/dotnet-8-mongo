using Infrastructure.Persistence.MongoDb;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            var repositoryType = typeof(TRepository);
            var repositoryName = repositoryType.Name;

            if (_repositories.ContainsKey(repositoryType.Name))
                return (TRepository)_repositories[repositoryName];

            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

            if (repositoryInstance == null)
            {
                throw new ArgumentNullException(nameof(repositoryInstance));
            }

            _repositories.Add(repositoryName, repositoryInstance);

            return (TRepository)_repositories[repositoryName];
        }

        public async Task UseTransactionAsync(Func<Task> transactionBody)
        {
            using (var session = await _context.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();

                    await transactionBody();

                    await session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}