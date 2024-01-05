
namespace Presentation.ViewModels.Order
{
    public class OrderViewModel : ViewModelBase
    {
        public IReadOnlyCollection<OrderItemViewModel> OrderItems { get; set; } = null!;
    }

    public class OrderItemViewModel
    {
        public long ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}