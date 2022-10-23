using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Services
{
    public static class StaticInfos
    {
        public const string connecitonString = @"Server=DESKTOP-LHDFEG6;Initial Catalog=BusTicket;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";

        public enum ScheduleStatus { reserved, empty }

        public static string[] Role = { "Sale Executive", "Manager" };
    }
}
