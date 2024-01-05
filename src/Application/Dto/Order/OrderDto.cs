
namespace Application.Dto.Order
{
    public class OrderDto : DtoBase
    {
        public string UserId { get; set; } = null!;

        //todo: do we need default value?
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
