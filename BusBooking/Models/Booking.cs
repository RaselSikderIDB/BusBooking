using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public DateTime? BookedDate { get; set; }
        public string BookedStatus { get; set; }
        public int? Price { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? ReturnAmount { get; set; }
        public bool? ReturnStatus { get; set; }
        public int? BusId { get; set; }
        public int? ScheduleId { get; set; }
    }
}
