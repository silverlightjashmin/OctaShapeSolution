using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaShapeSolution.Models
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "First Name Cannot be Blank")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Cannot be Blank")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name Cannot be Blank")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Id Cannot be Blank")]
        [EmailAddress]
        [Display(Name = "Email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please Type Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Type Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Choose Your Branch")]
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

    }
}
