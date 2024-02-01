
namespace Application.Dtos.Order
{
    public class OrderDto : DtoBase
    {
        public string UserId { get; set; } = null!;

        public string? UserId1 { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = null!;
    }
}
