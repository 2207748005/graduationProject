using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;
using System.Data.Entity;
using myCcourse.Filter;

namespace myCcourse.Controllers
{
    public class StudentController : Controller
    {
        CourseEntities6 db = new CourseEntities6();
        // GET: Student
        [CheckUser]
        public ActionResult Index()
        {
            //拿出session的值
            string name= Session["s_name"].ToString();
            View_student_class_college s = db.View_student_class_college.SingleOrDefault(t=>t.s_name.Contains(name));
            ViewBag.studentName = s.s_name;
            ViewBag.classNum = s.classNum;
            ViewBag.collegeName = s.collegeName;
            return View();
        }

        //个人信息
        public ActionResult Information() 
        {
            string name = Session["s_name"].ToString();
            student st = db.students.SingleOrDefault(t=>t.s_name==name);
            return View(st);
        }
        //清楚session并返回登录页面
        public ActionResult Quit()
        {
            Session["t_name"] = null;
            return RedirectToAction("Login", "Home");
        }
        //课表查询
        public ActionResult selectCourse() 
        {
            var list = db.teachers.Include("informs").ToList();
            return View(list);
        }

        public ActionResult insert(int t_id,int i_id) 
        {
            string name= Session["s_name"].ToString();
            student t = db.students.SingleOrDefault(a => a.s_name == name);
            shop s = new shop();
            db.Entry(s).State = EntityState.Added;
            s.i_id = i_id;
            s.t_id = t_id;
            s.s_id = t.s_id;
            db.shops.Add(s);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Inquire()
        {
            string name = Session["s_name"].ToString();
            student t = db.students.SingleOrDefault(a => a.s_name == name);
            var list = db.shops.Where(a => a.s_id == t.s_id).Include("teacher").Include("inform").ToList();
            return View(list);
        }
        //删除方法
        public ActionResult Delte(int shop_id) 
        {
            shop s = db.shops.SingleOrDefault(c => c.shop_id == shop_id);
            db.Entry(s).State = EntityState.Deleted;
            db.SaveChanges();
            //删除后的刷新操作
            string name =Session["s_name"].ToString();
            student t = db.students.SingleOrDefault(a => a.s_name == name);
            var list = db.shops.Where(a => a.s_id == t.s_id).Include("teacher").Include("inform").ToList();
            //viewbag.model = list;
            return PartialView("del",list);
        }
        public ActionResult Del() 
        {
            //刷新
            return View("");
        }
    }
}