
namespace Application.Dtos.Order
{
    public readonly record struct OrderItemDto(string ProductId, decimal UnitPrice, int Quantity);
}
