using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int? BusId { get; set; }
        public int? RouteId { get; set; }
        public DateTime? ActualDepartureTime { get; set; }
        public DateTime? ActualArrivalTime { get; set; }
        public bool? ScheduleCancel { get; set; }
        public string BusStatus { get; set; }
    }
}
