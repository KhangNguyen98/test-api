using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.FilterValidations
{
    public class EnterDateValidation:ActionFilterAttribute
    {
        //using Action Filter
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //param -> is that param of method using this class
            var ticket = context.ActionArguments["ticket"] as Ticket;
            bool isValid = true;
            if (ticket != null && !ticket.EnterDate.HasValue)
            {
                context.ModelState.AddModelError("EnterDate", "EnterDate is required");
                isValid = false;
            } else if (ticket != null && ticket.EnterDate.HasValue 
                && ticket.DueDate.HasValue
                && ticket.EnterDate.Value > ticket.DueDate.Value)
            {
                context.ModelState.AddModelError("DueDate", "Required DueDate > EnterDate");
                isValid = false;
            }
            if (!isValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
