using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Models
{
    public class Film
    {
        public string FilmID { get; set; }
        public string Name { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
