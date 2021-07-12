using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;
using System.Data.Entity;
using System.Data;
using myCcourse.Filter;

namespace myCcourse.Controllers
{
    public class TeacherController : Controller
    {
        CourseEntities6 db = new CourseEntities6();

        // GET: Teacher
        //添加过滤器
        [CheckUser]
        public ActionResult Index()
        {
            
            string name = Session["t_name"].ToString();
            teacher t = db.teachers.SingleOrDefault(a => a.t_name == name);
            ViewBag.teacherName = t.t_name;
            ViewBag.type = t.teacher_Type;
            return View();
        }
        //教师信息
        public ActionResult TeacherInfo() 
        {
            string name = Session["t_name"].ToString();
            teacher t = db.teachers.SingleOrDefault(s => s.t_name == name);
            return View(t);
        }

        public ActionResult CourseRelease() 
        {
            return View();
        }
        //发布课程的方法
        public ActionResult CourseReleaseSubmit(string message,string place) 
        {
            //拿出session保存的
            string name = Session["t_name"].ToString();
            teacher t = db.teachers.SingleOrDefault(c => c.t_name == name);
            inform i = new inform();
            db.Entry(i).State = EntityState.Added;
            i.message = message;
            i.place = place;
            i.datatime = DateTime.Now;
            i.in_id = t.t_id;
            db.informs.Add(i);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        //课程查询
        public ActionResult inquire() 
        {
            string name = Session["t_name"].ToString();
            teacher t = db.teachers.SingleOrDefault(c => c.t_name == name);
            //var list = db.teachers.Join(db.informs, a => a.t_id, b => b.in_id, (a, b) => new { b.i_id, b.message,b.place,b.datatime,a.t_name }).ToList();
            var list= db.teachers.Where(a => a.t_name == t.t_name).Include("informs").ToList();
            return View(list);
        }

        public ActionResult Del()
        {
            return View("");
        }
        //删除已选课的方法
        public ActionResult Delte(int i_id) 
        {
            inform i = db.informs.SingleOrDefault(c => c.i_id == i_id);
            shop s = db.shops.SingleOrDefault(e => e.i_id == i_id);
            if (s!=null) 
            {
                db.Entry(s).State = EntityState.Deleted;
            }
            db.Entry(i).State = EntityState.Deleted;
            int num = db.SaveChanges();
            //刷新
            string name = Session["t_name"].ToString();
            teacher t = db.teachers.SingleOrDefault(c => c.t_name == name);
            //var list = db.teachers.Join(db.informs, a => a.t_id, b => b.in_id, (a, b) => new { b.i_id, b.message, b.place, b.datatime, a.t_name }).ToList();
            var list = db.teachers.Where(a => a.t_name == t.t_name).Include("informs").ToList();
            return PartialView("Del",list) ;
        }
        //清楚session并返回登录页面
        public ActionResult Quit()
        {
            Session["s_name"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}