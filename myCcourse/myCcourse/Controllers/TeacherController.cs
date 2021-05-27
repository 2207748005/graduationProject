using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;

namespace myCcourse.Controllers
{
    public class TeacherController : Controller
    {
        CourseEntities4 db = new CourseEntities4();
        // GET: Teacher
        public ActionResult Index()
        {
            string name = Session["t_name"].ToString();
            View_teacer_Type t = db.View_teacer_Type.SingleOrDefault(a => a.t_name == name);
            ViewBag.teacherName = t.t_name;
            ViewBag.type = t.type;
            return View();
        }
    }
}