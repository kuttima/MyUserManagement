using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AQuIP.Admin.Models
{

    public class LoginViewModel
    {

        [Required(ErrorMessage = "User name is required.")]
        [EmailAddress(ErrorMessage = "User name has to be a valid email address.")]
        [Display(Name = "User Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pwd { get; set; }

    }

    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "User name is required.")]
        [EmailAddress(ErrorMessage = "User name has to be a valid email address.")]
        [Display(Name = "User Name")]
        public string UserLogin { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "Role")]
        public string Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage = "Organization name is required.")]
        [Display(Name = "Organization")]
        public string Organization { get; set; }

    }


    public class ResetPasswordViewModel : BaseModel
    {

        [Required(ErrorMessage = "User name is required.")]
        [EmailAddress(ErrorMessage = "User name has to be a valid email address.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ActivateDeactivateViewModel
    {

        [Required(ErrorMessage = "User name is required.")]
        [EmailAddress(ErrorMessage = "User name has to be a valid email address.")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please select a user status.")]
        [Display(Name = "User Status")]
        public string Status { get; set; }

    }
}