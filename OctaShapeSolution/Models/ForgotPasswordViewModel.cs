using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaShapeSolution.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "UserName Cannot be Blank")]

        [Display(Name = "UserName")]
        public string Email { get; set; }
    }
}
