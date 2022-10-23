using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class ScheduleDetail
    {
        public int ScheduleDetailsId { get; set; }
        public string SeatNo { get; set; }
        public int? BusId { get; set; }
        public int? ScheduleId { get; set; }
        public string ScheduleStatus { get; set; }
    }
}
