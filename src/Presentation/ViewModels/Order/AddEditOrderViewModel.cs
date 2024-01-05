using Application.Constants;
using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Order
{
    public class AddEditOrderViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "ITEMS")]
        public ICollection<AddEditOrderItemViewModel>? Items { get; set; }
    }

    public class AddEditOrderItemViewModel
    {
        [LongValidation]
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "PRODUCT_ID")]
        public long? ProductId { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "UNIT_PRICE")]
        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "QUANTITY")]
        public int? Quantity { get; set; }
    }
}
