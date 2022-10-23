using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class BookingDetail
    {
        public int BookingDetailsId { get; set; }
        public string SeatNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? BookingId { get; set; }
        public int? BusId { get; set; }
        public int? ScheduleId { get; set; }
        public int? ScheduleDetailsId { get; set; }
    }
}
