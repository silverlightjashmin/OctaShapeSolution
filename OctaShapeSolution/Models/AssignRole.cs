using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctaShapeSolution.Models
{
    public class AssignRole
    {
        [Required(ErrorMessage = "User Name Cannot be Blank")]
        [Display(Name = "User Name")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Role Cannot be Blank")]
        [Display(Name = "User Role")]
        public int RoleId { get; set; }
    }
}