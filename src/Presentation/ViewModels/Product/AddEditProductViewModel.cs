using Application.Constants;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Product
{
    public class AddEditProductViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "SKU")]
        public string? Sku { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "NAME")]
        public string? Name { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "UNIT_PRICE")]
        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "STOCK_QUANTITY")]
        public int? StockQuantity { get; set; }
    }
}
