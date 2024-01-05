
namespace Application.Dto.Order
{
    public class OrderItemDto
    {
        public string ProductId { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
