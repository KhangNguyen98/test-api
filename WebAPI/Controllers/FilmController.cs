using EntityFrameworkCore.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        private readonly TicketingSystemContext context;

        public FilmController(TicketingSystemContext context)
        {
            this.context = context;
        }
        //override attribute routing on controller
        //[HttpGet]
        //[Route("/api/films/{filmID}/{ticketID}")]
        //public IActionResult Get(string filmID, int ticketID)
        //{
        //    return Ok($"Reading filmID #{filmID} with ticket #{ticketID}");
        //}

        //using Model Biding

        //notice about tickets
        [Route("/api/films/{filmID}/tickets")]
        public IActionResult GetByUsingModelBinding([FromQuery]Ticket ticket)//this method is just used 4 experiment so i wont use ef
            //by default everything comes from query
        {
            if(ticket == null)
            {
                return BadRequest("Please fill ticket");
            }
            return Ok($"Reading filmID #{ticket.FilmID} with ticket " +
                $"#{ticket.TicketID} + title: #{ticket.Title}" +
                $" and description: #{ticket.Description}") ;
        }

    }
}
