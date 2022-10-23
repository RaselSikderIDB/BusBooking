using BusBooking.Models;
using BusBooking.Services;
using BusBooking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BusBooking.Services.StaticInfos;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        private BusTicketContext abc = null;

        public BookingController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
            abc = new BusTicketContext();
        }

        // GET api/booking/getall/1/1
        [HttpGet("[action]/{scheduleId}/{busId}")]
        public IEnumerable<object> getall(int scheduleId, int busId)
        {
            var list = (from bk in abc.Booking
                        join bs in abc.Bus on bk.BusId equals bs.BusId
                        join s in abc.Schedule on bk.ScheduleId equals s.ScheduleId
                        join r in abc.Route on s.RouteId equals r.RouteId
                        where bk.ScheduleId == scheduleId && bk.BusId == busId
                        select new
                        {
                            bk.BookingId,
                            bk.CustomerName,
                            bk.CustomerMobile,
                            bk.BookedDate,
                            bk.BookedStatus,
                            bk.Price,
                            bk.CancelDate,
                            bk.ReturnAmount,
                            bk.ReturnStatus,
                            bk.BusId,
                            bk.ScheduleId,
                            bs.BusName,
                            bs.BusType,
                            s.DepartureTime,
                            s.ArrivalTime,
                            s.RouteId,
                            r.RouteName,
                            Seat = (from bd in abc.BookingDetail where bd.BookingId == bk.BookingId select bd.BookingId).Count()
                        });
            return list;
        }


        [HttpGet("[action]/{scheduleId}/{busId}")]
        public IEnumerable<object> getscheduledetail(int scheduleId, int busId)
        {
            var listScheduleDetail = abc.ScheduleDetail.Where(sd => sd.ScheduleId == scheduleId && sd.BusId == busId).ToList();
            char[] alphas = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            var listVmScheduleDetail = new List<VmScheduleDetail>();
            foreach (var alpha in alphas)
            {
                var oVmScheduleDetail = new VmScheduleDetail();
                oVmScheduleDetail.Row = alpha.ToString();
                var listVmScheduleDetailSeat = new List<VmScheduleDetail.VmScheduleDetailSeat>();
                for (var i = 1; i <= 4; i++)
                {
                    string seatNo = alpha.ToString() + i;
                    var oSeatColor = abc.ScheduleDetail.Where(sd => sd.ScheduleId == scheduleId && sd.BusId == busId && sd.SeatNo == seatNo).FirstOrDefault();
                    if (oSeatColor != null)
                    {
                        var oVmScheduleDetailSeat = new VmScheduleDetail.VmScheduleDetailSeat();
                        oVmScheduleDetailSeat.ScheduleDetailsId = oSeatColor.ScheduleDetailsId;
                        oVmScheduleDetailSeat.BusId = oSeatColor.BusId;
                        oVmScheduleDetailSeat.ScheduleId = oSeatColor.ScheduleId;
                        oVmScheduleDetailSeat.ScheduleStatus = oSeatColor.ScheduleStatus;
                        oVmScheduleDetailSeat.SeatNo = oSeatColor.SeatNo;
                        oVmScheduleDetailSeat.SeatColor = oSeatColor.ScheduleStatus == StaticInfos.ScheduleStatus.reserved.ToString() ? "red" : "gray";
                        listVmScheduleDetailSeat.Add(oVmScheduleDetailSeat);
                    }

                    if (i == 2)
                    {
                        var oVmScheduleDetailSeat1 = new VmScheduleDetail.VmScheduleDetailSeat();
                        oVmScheduleDetailSeat1.SeatColor = "";
                        listVmScheduleDetailSeat.Add(oVmScheduleDetailSeat1);
                    }
                }
                oVmScheduleDetail.VmScheduleDetailSeats = listVmScheduleDetailSeat;
                listVmScheduleDetail.Add(oVmScheduleDetail);
            }
            return listVmScheduleDetail;
        }

        // POST: api/booking/save
        [HttpPost("[action]")]
        public async Task<object> save()
        {
            object result = null; string message = string.Empty; bool resstate = false;
            try
            {
                //Save
                var model = new Booking()
                {
                    BookingId = Convert.ToInt32(Request.Form["bookingId"].ToString()),
                    CustomerName = Request.Form["customerName"].ToString(),
                    CustomerMobile = Request.Form["customerMobile"].ToString(),
                    BookedDate = DateTime.Now,
                    BookedStatus = Request.Form["bookedStatus"].ToString(),
                    Price = Request.Form["price"].ToString() == "" ? (int?)null : Convert.ToInt32(Request.Form["price"].ToString()),
                    CancelDate = (DateTime?)null,
                    ReturnAmount = Request.Form["returnAmount"].ToString() == "" ? (int?)null : Convert.ToInt32(Request.Form["returnAmount"].ToString()),
                    ReturnStatus = Request.Form["returnStatus"].ToString() == "" ? (bool?)null : Convert.ToBoolean(Request.Form["returnStatus"].ToString()),
                    BusId = Request.Form["busId"].ToString() == "" ? (int?)null : Convert.ToInt32(Request.Form["busId"].ToString()),
                    ScheduleId = Request.Form["scheduleId"].ToString() == "" ? (int?)null : Convert.ToInt32(Request.Form["scheduleId"])
                };

                var obj = abc.Booking.Where(s => s.BookingId == model.BookingId).FirstOrDefault();
                if (obj == null)
                {
                    abc.Booking.Add(model);
                    await abc.SaveChangesAsync();

                    var listBookingDetail = new List<BookingDetail>();
                    var listSchedule = abc.ScheduleDetail.Where(w => w.ScheduleId == model.ScheduleId).ToList();
                    var ids = Request.Form["ids"].ToString();
                    List<string> arrID = new List<string>();
                    if (ids.Contains(","))
                    {
                        var arr = ids.Split(',');
                        foreach (var item in arr)
                        {
                            arrID.Add(item);
                        }
                    }
                    else
                    {
                        arrID.Add(ids);
                    }
                    //string[] arrID = ids.Contains(",") ? ids.Split(',') : new string[0];
                    foreach (var id in arrID)
                    {
                        int nid = Convert.ToInt32(id);
                        var ScheduleDetails = listSchedule.Where(w => w.ScheduleDetailsId == nid).FirstOrDefault();
                        if (ScheduleDetails != null)
                        {
                            var oBookingDetail = new BookingDetail();
                            //oBookingDetail.BookingDetailsId PK
                            oBookingDetail.SeatNo = ScheduleDetails.SeatNo;
                            oBookingDetail.CustomerName = model.CustomerName;
                            oBookingDetail.CustomerMobile = model.CustomerMobile;
                            oBookingDetail.BookingId = model.BookingId;
                            oBookingDetail.BusId = ScheduleDetails.BusId;
                            oBookingDetail.ScheduleId = ScheduleDetails.ScheduleId;
                            oBookingDetail.ScheduleDetailsId = ScheduleDetails.ScheduleDetailsId;
                            listBookingDetail.Add(oBookingDetail);

                            ScheduleDetails.ScheduleStatus = ScheduleStatus.reserved.ToString();
                            await abc.SaveChangesAsync();
                        }
                    }
                    abc.BookingDetail.AddRange(listBookingDetail);
                    await abc.SaveChangesAsync();

                    await abc.Booking.AddAsync(model);

                    message = "Added successfully.";
                    resstate = true;

                    await _hubContext.Clients.All.BroadcastMessage("success", "Seat Booked!");
                }

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

        // DELETE api/booking/deletebyid/1
        [HttpDelete("[action]/{id}")]
        public async Task<object> deletebyid(int id)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            try
            {
                var list = abc.BookingDetail.Where(s => s.BookingId == id).ToList();
                foreach (var o in list)
                {
                    var ScheduleDetails = abc.ScheduleDetail.Where(w => w.ScheduleDetailsId == o.ScheduleDetailsId).FirstOrDefault();
                    if (ScheduleDetails != null)
                    {
                        ScheduleDetails.ScheduleStatus = ScheduleStatus.empty.ToString();
                        await abc.SaveChangesAsync();
                    }
                }
                abc.BookingDetail.RemoveRange(list);
                var obj = abc.Booking.Where(s => s.BookingId == id).FirstOrDefault();
                abc.Booking.Remove(obj);
                await abc.SaveChangesAsync();
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



        [HttpGet("[action]/{startpoint}/{endpoint}")]
        public async Task<object> getschedule(string startpoint, string endpoint)
        {
            object result = null; string message = string.Empty; bool resstate = false; List<VmSchedule> listVmSchedule = null;
            try
            {
                var service = new MsSqlService();
                var dt = await service.Get(StaticInfos.connecitonString, "EXEC SP_GetSchedule @startPoint = '" + startpoint + "', @endPoint = '" + endpoint + "'", true);
                listVmSchedule = Conversion.ConvertDataTableToObject<VmSchedule>(dt);
                message = "Data retrieve successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            result = new
            {
                message,
                resstate,
                listVmSchedule
            };

            return result;
        }

       

    }
}
