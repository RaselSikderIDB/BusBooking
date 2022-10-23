using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int? RouteId { get; set; }
        public int? BusId { get; set; }
        public int? UnitPrice { get; set; }
    }
}
