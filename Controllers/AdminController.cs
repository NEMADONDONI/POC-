using LeaveManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Leave()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveLeave(Leave leave)
        {
            LeaveManagementSystemEntities db = new LeaveManagementSystemEntities();
            bool isSuccess = true;

            if (leave.Id > 0)
            {
                db.Entry(leave).State = EntityState.Modified;
            }
            else
            {
                db.Leaves.Add(leave);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalcLeaveTaken()
        {
            return View();
        }


        public ActionResult ViewAllLeave()
        {
            LeaveManagementSystemEntities db = new LeaveManagementSystemEntities();
            var listofData = db.Leaves.ToList();
            return View(listofData);
        }

        [HttpGet]
        public JsonResult GetAllLeaves()
        {
            LeaveManagementSystemEntities db = new LeaveManagementSystemEntities();
            var dataList = db.Leaves.ToList();
            var data = dataList.Select(x => new {
                Username = x.Username,
                LeaveStartDate = x.LeaveStartDate.ToString(),
                LeaveEndDate = x.LeaveEndDate.ToString(),
                Reason = x.Reason,
                TotalLeaveDaysTaken = x.TotalLeaveDaysTaken,
                TotalLeaveLeft =x.TotalLeaveLeft,
                LeaveType = x.LeaveType,

            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}