using System;
using System.Web.Mvc;
using Matrimonial.ClientModels;
using Matrimonial.Mappers;
using Matrimonial.Models;

namespace Matrimonial.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _entityUser = new UserRepository();
        IAdminRepositiry _adminRepositiry = new AdminRepository();
        public ActionResult LoginUser(UserProfileModel user)
        {
            
            var temp = _entityUser.GetUser(user.Email);
            if (_entityUser.IsUser(user.MapClientToServer()) && temp != null)
            {
                Session["user"] = temp.MapServerToClient();
                Session.Timeout = 30;
            }
                
            else
            {
                ViewBag.isUser = false;
                return View("Login");
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        //public ActionResult LoginAdmin(Admin admin)
        //{
        //    Admin temp;
        //    temp = _adminRepositiry.GetAdmin(admin.Email);
        //    if (_adminRepositiry.IsAdmin(admin) && temp != null)
        //    {
        //        Session["admin"] = temp;
        //        Session.Timeout = 30;
        //    }
                
        //    else
        //    {
        //        ViewBag.isAdmin = false;
        //        return RedirectToAction("Login","Admin");
        //    }
            
        //    return RedirectToAction("Index", "Admin");
        //}
        public ActionResult Login()
        {
            var u = (UserProfileModel) Session["user"];
            if(u == null)
                return View();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterIn(UserProfileModel user)
        {
            user.IsVerified = "false";
            user.Age = DateTime.Now.Subtract(user.DateOfBirth).Days/365;
            ViewBag.UserExists = !_entityUser.AddNew(user.MapClientToServer());
            
            return View("Register");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.RemoveAll();
            //Session["user"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult LogOutAdmin()
        {
            //Session["admin"] = null;
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}