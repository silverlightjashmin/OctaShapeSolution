using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaShapeSolution.Models
{ 
   public class AuthenticateViewModel
    {
        [Required(ErrorMessage = "Please Type UserName")]
        [Display(Name = "User name")]
        public string UserName  { get; set; }

        [Required(ErrorMessage = "Please Type Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
