using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int? BusId { get; set; }
        public int? UnitPrice { get; set; }
    }
}
