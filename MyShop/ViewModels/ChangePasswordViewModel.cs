using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [StringLength(40, MinimumLength = 10, ErrorMessage = "The {0} must be atleast {2} and maximum {1} characters long")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "The new password must be same as confirm new password.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }

}
