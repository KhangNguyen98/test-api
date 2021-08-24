using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.ModelValidations
{
    public class DueDateInTheFutureValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if(ticket != null && ticket.TicketID == null)
            {
                if(ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now)
                {
                    return new ValidationResult("Due date has to be the future");
                } 
            }
            return ValidationResult.Success;
        }
    }
}
