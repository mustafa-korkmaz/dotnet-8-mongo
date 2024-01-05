using Application.Constants;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Identity
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [EmailAddress(ErrorMessage = ValidationErrorCode.EmailNotValid)]
        [Display(Name = "EMAIL")]
        public string? Email { get; set; }

        //[StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 4)]
        [Display(Name = "NAME_SURNAME")]
        public string? NameSurname { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "PASSWORD")]
        public string? Password { get; set; }
    }
}
