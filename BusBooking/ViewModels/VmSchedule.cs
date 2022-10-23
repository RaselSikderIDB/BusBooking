using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.ViewModels
{
    public class VmSchedule : Schedule
    {
        public int Booked { get; set; }
        public string RouteName { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string BusName { get; set; }
        public string BusType { get; set; }
        public int NoOfSeat { get; set; }
        public int AvailableSeat { get; set; }
        public int UnitPrice { get; set; }
    }
}
