using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myCcourse.Models;

namespace myCcourse.Controllers
{
    public class HomeController : Controller
    {
        //实例化模型类
        CourseEntities4 db = new CourseEntities4();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        // Post: Home
        //管理员登录方法
        public ActionResult adminLogin(string username,string password) 
        {
            Admin a = db.Admins.SingleOrDefault(t => t.name == username);
            if (a!=null)
            {
                if (a.password == password)
                {
                    //把用户存到session里面
                    Session["LoginUserName"] = a.name;
                    return RedirectToAction("index", "Admin");
                }
                else {
                    ViewBag.Error = "密码错误！";
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Error = "用户名错误";
                return View("Login");
            }
            
        }
        //学生验证登录
        public ActionResult studentLogin(string username, string password)
        {
            student a = db.students.SingleOrDefault(t => t.s_name==username);
            if (a != null)
            {
                if (a.s_password == password)
                {
                    //把用户存到session里面
                    Session["s_name"] = a.s_name;
                    return RedirectToAction("index", "Student");
                }
                else
                {
                    ViewBag.Error2 = "密码错误！";
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Error2 = "用户名错误";
                return View("Login");
            }
            
        }
        //验证教师登录
        public ActionResult teacherLogin(string username,string password)
        {
            teacher a = db.teachers.SingleOrDefault(t => t.t_name == username);
            if (a != null)
            {
                if (a.t_password == password)
                {
                    //把用户存到session里面
                    Session["t_name"] = a.t_name;
                    return RedirectToAction("index", "Teacher");
                }
                else
                {
                    ViewBag.Error1 = "密码错误！";
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Error1 = "用户名错误";
                return View("Login");
            }
            
        }
        public ActionResult index() 
        {
            return View();
        }
    }
}