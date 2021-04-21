using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentMange.Models;

namespace StudentMange.Controllers
{
    public class SignInOrUpController : Controller
    {
        private User.UserDBContent db= new User.UserDBContent();
        
        // GET: SignInOrUp
        public ActionResult Login()
        {
            adduser();
            ViewBag.msg ="";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//有效的校验码，防止被恶意攻击
        public ActionResult Login([Bind(Include = "Myname,Passsword")] User user)
        {
           
            
            if (ModelState.IsValid)//判断模型状态是否有效
            {
                User u = db.users.Find(user.Myname);
                if (u == null)
                {
                    ViewBag.msg = "null";

                } else if (user.Passsword !=u.Passsword)
                {
                    ViewBag.msg = "no";
                }
                else
                {
                    
                    Session["Username"] = user.Myname;
                    HttpCookie cookie = new HttpCookie("user");
                    cookie.Expires = DateTime.Now.AddDays(7);
                    cookie["Username"] = user.Myname;
                    cookie["Password"] = user.Passsword.ToString();
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Student");
                }

                
                
            }
            return View();

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//有效的校验码，防止被恶意攻击
        public ActionResult SignIn([Bind(Include = "Myname,Passsword")] User user)
        {

            if (ModelState.IsValid)//判断模型状态是否有效
            {
                db.users.Add(user);//将实体传入数据库
                db.SaveChanges();//保存修改
                return RedirectToAction("Login");//重定向去INDEX视图


            }
            return View();


        }

        public  ActionResult SignIn()
        {
            return View();
        }
        public void adduser() { 
         var u = new User()
        {
            Myname = "user",
            Passsword = 0
        };
            db.users.Add(u);
            
        }
    }
}