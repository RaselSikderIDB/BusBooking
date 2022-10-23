using BusBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private BusTicketContext _ctx = null;
        public AccountController()
        {
            _ctx = new BusTicketContext();
        }

        // POST: api/account/login
        [HttpPost("[action]")]
        public async Task<object> login()
        {
            object result = null; string message = string.Empty; bool resstate = false; object resdata = null;
            try
            {
                var model = new CmnUser()
                {
                    UserName = Convert.ToString(Request.Form["userName"]),
                    UserPass = Convert.ToString(Request.Form["userPass"]),
                };

                var obj = _ctx.CmnUser.Where(u => u.UserName == model.UserName && u.UserPass == model.UserPass).FirstOrDefault();
                if (obj == null)
                {
                    resstate = false;
                    message = "Login failed!";
                }
                else
                {
                    obj.UserPass = string.Empty;
                    resdata = obj;
                    message = "Login successed!";
                    resstate = true;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            result = new
            {
                message,
                resstate,
                resdata
            };

            return result;
        }
    }
}
