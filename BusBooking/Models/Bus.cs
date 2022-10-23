using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Bus
    {
        public int BusId { get; set; }
        public string BusName { get; set; }
        public string BusType { get; set; }
        public int? NoOfSeat { get; set; }
        public string LicenseNo { get; set; }
        public bool? FitnessStatus { get; set; }
    }
}
