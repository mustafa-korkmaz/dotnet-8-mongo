namespace Application.Dtos.Order
{
    public record OrderDto(ICollection<OrderItemDto> Items, DateTime CreatedAt) : DtoBase(CreatedAt)
    {
        public string UserId { get; set; } = null!;
    }
}