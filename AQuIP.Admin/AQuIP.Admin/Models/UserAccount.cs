using System.ComponentModel.DataAnnotations;

namespace AQuIP.Admin.Models
{
    public class UserAccount : BaseModel
    {
        [Key]
        public int UserId { get;}

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [EmailAddress(ErrorMessage = "User name has to be a valid email address.")]
        [Display(Name = "User Name" )]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Organization name is required.")]
        [Display(Name = "Organization")]
        public string Name { get; set; }

    }   
   
}