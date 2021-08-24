using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.ModelValidations
{
    public class DueDateForTicketWhenHavingOwnerValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if(ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (!ticket.DueDate.HasValue)
                    return new ValidationResult("Due date is required cuz having owner");
            }
            return ValidationResult.Success;
        }
    }
}
