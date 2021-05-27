using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;

namespace myCcourse.Controllers
{
    public class AdminController : Controller
    {
        CourseEntities4 db = new CourseEntities4();
        // GET: Admin
        public ActionResult Index()
        {
            List<View_teacer_Type> list = db.View_teacer_Type.ToList();
            ViewData.Model = list;
            return View("");
        }
        public ActionResult AllSelsect()
        {

            List<View_student_class_college> list = db.View_student_class_college.ToList();

            return PartialView("All", list);
        }
        public ActionResult All() 
        {
            return View("");
        }
        public ActionResult Teacher() 
        {
            return View();
        }

        public ActionResult TeacherSelect() 
        {
            List<View_teacer_Type> list = db.View_teacer_Type.ToList();
            return PartialView("Teacher", list);
        }

        public ActionResult studentTable() 
        {
            //List<Viewt_class_college> list = db.Viewt_class_college.ToList();
            return View("");
        }

        public ActionResult Student()
        {
            //查询视图
            List<Viewt_class_college> list = db.Viewt_class_college.ToList();
            return PartialView("studentTable", list);
        }

        public ActionResult collegeTable() 
        {
            return View("");
        }
        public ActionResult College() 
        {
            List<college> list = db.colleges.ToList();
            return PartialView("collegeTable", list);
        }
    }
}