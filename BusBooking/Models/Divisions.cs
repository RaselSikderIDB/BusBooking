using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Divisions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BnName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
