using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;

namespace myCcourse.Controllers
{
    public class StudentController : Controller
    {
        CourseEntities4 db = new CourseEntities4();
        // GET: Student
        public ActionResult Index()
        {
            string name= Session["s_name"].ToString();
            View_student_class_college s = db.View_student_class_college.SingleOrDefault(t=>t.s_name.Contains(name));
            ViewBag.studentName = s.s_name;
            ViewBag.classNum = s.classNum;
            ViewBag.collegeName = s.collegeName;
            return View();
        }
    }
}