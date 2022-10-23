using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Models
{
    public partial class TicketSale
    {
        public int TicketSaleId { get; set; }
        public DateTime? SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? Price { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? ReturnAmount { get; set; }
        public bool? ReturnStatus { get; set; }
        public int? ScheduleId { get; set; }
        public int? BusId { get; set; }
    }
}
