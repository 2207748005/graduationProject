using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;
using myCcourse.Filter;
using System.Data.Entity;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace myCcourse.Controllers
{
    public class AdminController : Controller
    {
        CourseEntities6 db = new CourseEntities6();
        // GET: Admin
        [CheckUser]

        public ActionResult Index()
        {
            List<teacher> list = db.teachers.ToList();
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
            List<teacher> list = db.teachers.ToList();
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
        //给学院表传list
        public ActionResult College() 
        {
            List<college> list = db.colleges.ToList();
            return PartialView("collegeTable", list);
        }
        //清楚session并返回登录页面
        public ActionResult Quit() 
        {
            Session["LoginUserName"] = null;
            return RedirectToAction("Login","Home");
        }

        //删除学生
        public ActionResult Delte(int s_id)
        {
            student s = db.students.SingleOrDefault(t => t.s_id == s_id);
            var shops = db.shops.Where(a => a.s_id == s_id).ToList();
            //遍历shop表里面的包含有s_id的数据进行一条一条删除
            foreach (var item in shops)
            {
                shop sh = db.shops.SingleOrDefault(z => z.shop_id == item.shop_id);
                db.Entry(sh).State = EntityState.Deleted;
                db.SaveChanges();
            }
            db.Entry(s).State = EntityState.Deleted;
            db.SaveChanges();
            //刷新并传值给分部视图
            List<View_student_class_college> list = db.View_student_class_college.ToList();
            return PartialView("all", list);
        }
        //insert 弹出层视图
        public ActionResult InsertStu() 
        {
            return View("");
        }
        public ActionResult insert(student s,string grade,string classNum,int cl_id)
        {
            
            college e = db.colleges.SingleOrDefault(t=>t.c_id==cl_id);
            //判断用户输入的学院是否不存在存在则进行下一步
            if (e!=null)
            {
                cust cu = db.custs.SingleOrDefault(a => a.classNum == classNum&&a.grade==grade&&a.cl_id==cl_id);
                if (cu==null)
                {
                    cust c = new cust();
                    c.cl_id = cl_id;
                    c.classNum = classNum;
                    c.grade = grade;
                    db.Entry(c).State = EntityState.Added;
                    db.SaveChanges();
                    student st = new student();
                    st.s_name = s.s_name;
                    st.s_password = s.s_password;
                    st.s_phone = s.s_phone;
                    st.s_sex = s.s_sex;
                    st.s_age = s.s_age;
                    st.s_address = s.s_address;
                    st.st_id = c.cs_id;
                    db.Entry(st).State = EntityState.Added;
                    db.SaveChanges();
                    List<View_student_class_college> list1 = db.View_student_class_college.ToList();
                    return RedirectToAction("index");
                }
                else
                {
                    student std = new student();
                    std.s_name = s.s_name;
                    std.s_password = s.s_password;
                    std.s_phone = s.s_phone;
                    std.s_sex = s.s_sex;
                    std.s_age = s.s_age;
                    std.s_address = s.s_address;
                    std.st_id = cu.cs_id;
                    db.Entry(std).State = EntityState.Added;
                    db.SaveChanges();
                    List<View_student_class_college> list2 = db.View_student_class_college.ToList();
                    return RedirectToAction("index");
                }
                
            }
            List<View_student_class_college> list3 = db.View_student_class_college.ToList();
            return RedirectToAction("index");
        }

        public ActionResult insertTea() 
        {
            return View();
        }
        //传一个json数据在mvc里面
        public JsonResult json() 
        {
            IEnumerable<college> list= db.colleges.ToList();
            //
            //db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult jsondata() 
        {
            return View("");
        }
    }
}