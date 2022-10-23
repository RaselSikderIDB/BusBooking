using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string TransactionId { get; set; }
        public bool? IsComplete { get; set; }
        public int? RouteId { get; set; }
        public int? ScheduleId { get; set; }
        public int? TicketId { get; set; }
    }
}
