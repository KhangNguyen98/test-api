using EntityFrameworkCore.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.FilterValidations;
using WebAPI.Model;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/tickets")]


    //use this if u want just this work with Resource Filter
    //else set Resource Filter at Startup.cs
    //[Version1Expired]
    public class TicketController:Controller
    {
        private readonly TicketingSystemContext context;

        public TicketController(TicketingSystemContext context)
        {
            this.context = context;
        }

        [HttpGet]
       // [Route("api/tickets")]
        public IActionResult Get()
        {
            return Ok(context.Tickets.ToList());
        }

 //       [HttpGet("api/tickets/{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id == 0)// if we dont declare it will set default value
            {
                return Ok(context.Tickets.Where(element => element.TicketID == 0));
              //  return Ok($"Reading ticket with default value #{id}");
            }
            var ticket = context.Tickets.Find(id);
            if(ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
            //string interpolation
            //return Ok($"Reading ticket #{id}");
        }


        //Problem if we have two different endpoint(related to BusinessLogic)
        //but same purpose
        //If we use Data Anotation then two different endpoint can't exist at my app
        //->using ActionFilter
        [HttpPost]
        [Route("/api/v1/tickets")]
        public IActionResult CreateVersion1([FromBody]Ticket ticket)
        {
            if ( context.Tickets.Find(ticket.FilmID) != null)
            {
                return BadRequest("Duplicate id");
            }
            context.Tickets.Add(ticket);
            context.SaveChanges();
            return Ok(ticket);
        }


        //Version 2's different than ver 1 that we don't allow
        //enterName is null but ver 1 can
        //if we use DataAnotation on EnterName
        //that we can just choose 1 choice
        [HttpPost]
        [Route("/api/v2/tickets")]
        //using ActionFilter
        [EnterDateValidation]
        public IActionResult CreateVersion2([FromBody] Ticket ticket)
        {
            if (context.Tickets.Find(ticket.TicketID) != null)
            {

                return BadRequest("Duplicate id");
            }
            context.Tickets.Add(ticket);
            context.SaveChanges();
            return Ok(ticket);
        }

        //Test Resource Filter
        [HttpGet]
        [Route("/api/ver1")]
        public IActionResult MethodVersion1()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Ticket ticket)
        {
            if(ticket == null || ticket.TicketID != id)
            {
                return BadRequest("Invalid/Duplicate id");
            }
            context.Update(ticket);
            context.SaveChanges();
            return Ok(ticket);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return Ok(ticket);
        }
    }
}
