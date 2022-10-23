using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Districts
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string BnName { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public string Website { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
