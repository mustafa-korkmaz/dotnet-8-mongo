namespace Application.Dtos.Order
{
    public record OrderDto(ICollection<OrderItemDto> Items) : DtoBase
    {
        public string UserId { get; set; } = null!;
    }
}