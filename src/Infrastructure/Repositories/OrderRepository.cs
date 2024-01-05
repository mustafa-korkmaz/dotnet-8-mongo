using Domain.Aggregates.Order;
using Infrastructure.Persistence.MongoDb;

namespace Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IMongoContext context) : base(context)
        {

        }
    }
}