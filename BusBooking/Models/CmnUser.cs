using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class CmnUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
