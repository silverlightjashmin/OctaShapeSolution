using OctaShape.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctaShapeSolution.Areas.CardInformationSystem.Models
{
    public class CardRequestDate: IValidatableObject
    {
        public CardRequestDate()
        {
            Card_RequestDetail = new List<Card_RequestDetail>();
        }

        [Required(ErrorMessage ="Must Enter Date")]
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage ="Must Enter Date")]
       
        [DataType(DataType.Date)]
        [Display(Name ="End Date")]
        
        public DateTime EndDate { get; set; }

        public List<Card_RequestDetail> Card_RequestDetail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return
                  new ValidationResult(errorMessage: "EndDate must be greater than StartDate",
                                       memberNames: new[] { "EndDate" });
            }
        }
    }
}