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
    public class CmnUserController : ControllerBase
    {
        private BusTicketContext _ctx = null;
        public CmnUserController()
        {
            _ctx = new BusTicketContext();
        }

        [HttpGet("[action]")]
        public IEnumerable<string> getallrole()
        {
            return Services.StaticInfos.Role;
        }

        [HttpGet("[action]")]
        public IEnumerable<CmnUser> getall()
        {
            return _ctx.CmnUser;
        }

        [HttpPost("[action]")]
        public async Task<object> save()
        {
            object result = null; string message = string.Empty; bool resstate = false;
            try
            {
                //Save
                var model = new CmnUser()
                {
                    UserId = Convert.ToInt32(Request.Form["userId"].ToString()),
                    UserName = Request.Form["userName"].ToString(),
                    UserPass = Request.Form["userPass"].ToString(),
                    Role = Request.Form["role"].ToString(),
                    FullName = Request.Form["fullName"].ToString(),
                    Mobile = Request.Form["mobile"].ToString(),
                    Email = Request.Form["email"].ToString()
                };

                var obj = _ctx.CmnUser.Where(s => s.UserId == model.UserId).FirstOrDefault();
                if (obj == null)
                {
                    _ctx.CmnUser.Add(model);
                    message = "Added successfully.";
                }
                else
                {
                    obj.UserName = model.UserName;
                    obj.UserPass = model.UserPass;
                    obj.Role = model.Role;
                    obj.FullName = model.FullName;
                    obj.Mobile = model.Mobile;
                    obj.Email = model.Email;
                    message = "Update successfully.";
                }
                await _ctx.SaveChangesAsync();
                resstate = true;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            result = new
            {
                message,
                resstate
            };

            return result;
        }

        // DELETE api/cmnuser/deletebyid/1
        [HttpDelete("[action]/{id}")]
        public async Task<object> deletebyid(int id)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            try
            {
                var obj = _ctx.CmnUser.Where(s => s.UserId == id).FirstOrDefault();
                _ctx.CmnUser.Remove(obj);
                await _ctx.SaveChangesAsync();
                message = "Remove successfully.";
                resstate = true;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            result = new
            {
                message,
                resstate
            };

            return result;
        }
    }
}
