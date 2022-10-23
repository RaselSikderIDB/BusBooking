using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class ReturnPolicy
    {
        public int ReturnPolicyId { get; set; }
        public int? ReturnHour { get; set; }
        public int? ReturnPercent { get; set; }
    }
}
