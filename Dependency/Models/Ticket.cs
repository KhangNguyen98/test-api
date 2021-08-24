using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.ModelValidations;

namespace WebAPI.Model
{
    public class Ticket
    {
       // [FromQuery(Name = "ticketID")]
        public int TicketID { get; set; }
        [FromRoute( Name = "filmID")]
        public string FilmID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Owner { get; set; }
        [DueDateForTicketWhenHavingOwnerValidation]
        [DueDateInTheFutureValidation]
        public DateTime? DueDate { get; set; }

        public DateTime? EnterDate { get; set; }

        public Film Film { get; set; }
    }
}
