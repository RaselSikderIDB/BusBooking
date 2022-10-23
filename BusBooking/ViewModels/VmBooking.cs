using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.ViewModels
{
    public class VmBooking
    {
        public int? BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public DateTime? BookedDate { get; set; }
        public string BookedStatus { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CancelDate { get; set; }
        public decimal? ReturnAmount { get; set; }
        public string ReturnStatus { get; set; }
        public int? BusId { get; set; }
        public int? ScheduleId { get; set; }
        public string BusName { get; set; }
        public string BusType { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int? RouteId { get; set; }
        public string RouteName { get; set; }
        public int? Seat { get; set; }
        public int? UnitPrice { get; set; }
    }
}
