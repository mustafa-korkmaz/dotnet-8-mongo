namespace Presentation.ViewModels.Order
{
    public class OrderViewModel : ViewModelBase
    {
        public IReadOnlyCollection<OrderItemViewModel> Items { get; set; } = null!;
    }

    public class OrderItemViewModel
    {
        public string ProductId { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}