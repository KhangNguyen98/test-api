using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Model;
using WebAPI.Models;

namespace EntityFrameworkCore.DbContexts
{
    public class TicketingSystemContext:DbContext
    {
        //passing the options that we can configure that in Startup class
        public TicketingSystemContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Film> Films { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().HasMany(film => film.Tickets)
                                        .WithOne(ticket => ticket.Film)
                                        .HasForeignKey(ticket => ticket.FilmID)
                                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Film>().HasData(
                 new Film { FilmID = "235", Name = "Finland girl"},
                 new Film { FilmID = "1996", Name = "The girl from my heart"}
                ) ;

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { TicketID = 1, FilmID = "235", Description = "Whatever", Title = "Sale 15%" },
                new Ticket { TicketID = 2, FilmID = "1996", Description = "Whatever", Title = "Sale 15%" }
                );
        }
    }
}
