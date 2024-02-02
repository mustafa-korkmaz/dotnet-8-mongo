namespace Application.Dtos.Product
{
    public record ProductDto(string Sku, string Name, decimal UnitPrice, int StockQuantity) : DtoBase;
}