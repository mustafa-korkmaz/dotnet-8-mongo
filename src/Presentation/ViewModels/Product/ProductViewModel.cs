namespace Presentation.ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
        public string Sku { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }
    }
}
