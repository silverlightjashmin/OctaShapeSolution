using OctaShape.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OctaShapeSolution.Areas.CardInformationSystem.Models
{
    public class CardActivateDate:IValidatableObject
    {
        public CardActivateDate()
        {
            Card_ReceivedDetail  = new List<Card_ReceivedDetails>();

        }
        [Required(ErrorMessage = "Must Enter Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Must Enter Date")]

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]

        public DateTime EndDate { get; set; }

        public List<Card_ReceivedDetails> Card_ReceivedDetail { get; set; } 

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