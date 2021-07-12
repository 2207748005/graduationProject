using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Mvc;先引入
namespace myCcourse.Filter
{
    //第二步继承接口
    public class CheckUserAttribute:ActionFilterAttribute
    {
        //OnActionExecuting在action之前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断是否有用户登录，如果有用户登录就放行，如果没有就重定向到主页，让用户登录
            if (HttpContext.Current.Session["LoginUserName"] ==null && HttpContext.Current.Session["t_name"] == null && HttpContext.Current.Session["s_name"] == null)
            {
                filterContext.Result = new RedirectResult("/home/Login");
            }
        }
    }
}