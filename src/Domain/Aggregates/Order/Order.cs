
namespace Domain.Aggregates.Order
{
    public class Order : Document
    {
        public string UserId { get; private set; }

        public decimal Price => Items.Sum(x => x.GetPrice());

        private ICollection<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items
        {
            get => _items.ToList();
            private set => _items = value.ToList();
        }

        public Order(string id, string userId, DateTime createdAt) : base(id)
        {
            _items = new List<OrderItem>();
            UserId = userId;
            CreatedAt = createdAt;
        }

        public void AddItem(string productId, decimal unitPrice, int quantity, DateTimeOffset createdAt)
        {
            _items.Add(new OrderItem(productId, unitPrice, quantity));
        }
    }
}
