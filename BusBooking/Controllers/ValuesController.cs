using BusBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class valuesController : ControllerBase
    {
        private BusTicketContext abc = null;

        public valuesController()
        {
            abc = new BusTicketContext();
        }

        [HttpGet("[action]")]
        public object index()
        {
            object result = null; string message = string.Empty;
            message = "API running...";

            result = new
            {
                message
            };
            return result;
        }

        [HttpGet("[action]")]
        public IEnumerable<Divisions> GetDivisions()
        {
            return abc.Divisions.OrderBy(o => o.Name);
        }

        [HttpGet("[action]")]
        public IEnumerable<Districts> GetDistrictsByDivision(int divisionId)
        {
            return abc.Districts.Where(w => w.DivisionId == divisionId).OrderBy(o => o.Name);
        }
    }
}
