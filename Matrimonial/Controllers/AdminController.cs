using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Matrimonial.ClientModels;
using Matrimonial.Mappers;
using Matrimonial.Models;

namespace Matrimonial.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        IAdminRepositiry _adminRepositiry = new AdminRepository();
        IUserRepository _userRepository = new UserRepository();
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Login", "Admin");

            List<UserProfileModel> list = _userRepository.GetAllProfiles().ToList().Select(x=>x.MapServerToClient()).ToList();
            return View(list);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginAdmin(Admin admin)
        {
            Admin temp;
            temp = _adminRepositiry.GetAdmin(admin.Email);
            if (_adminRepositiry.IsAdmin(admin) && temp != null)
            {
                Session["admin"] = temp;
                Session.Timeout = 30;
            }

            else
            {
                ViewBag.isAdmin = false;
                return View("Login");
            }

            return RedirectToAction("Index");
        }

        public ActionResult AdminProfile()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Login", "Admin");

            return View(Session["admin"]);
        }
        public ActionResult EditProfile(Admin a)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Login", "Admin");
            try
            {
                HttpPostedFileBase file;
                file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string path = Server.MapPath("~/UserUploads/Admin/" + file.FileName);
                    file.SaveAs(path);
                    a.ProfileImage = file.FileName;
                }
            }
            catch
            {
                
            }
            
            _adminRepositiry.EditProfile(a);
            Session["admin"] = _adminRepositiry.GetAdmin(a.Id);
            return View("AdminProfile",Session["admin"]);
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Login", "Admin");

            if (_adminRepositiry.ChangePassword(id, oldPassword, newPassword))
                ViewBag.changed = true;
            else
                ViewBag.changed = false;

            Session["admin"] = _adminRepositiry.GetAdmin(id);
            return View("AdminProfile", Session["admin"]);
        }

        public ActionResult Users()
        {
            List<UserProfileModel> list = _userRepository.GetAllProfiles().ToList().Select(x=>x.MapServerToClient()).ToList();
            return View(list);

        }
        public ActionResult DeleteUser(int id)
        {
            if (_userRepository.DeleteUser(id))
            {
                ViewBag.err = true;
            }
            else
            {
                ViewBag.err = false;
            }

            List<UserProfileModel> list = _userRepository.GetAllProfiles().ToList().Select(x=>x.MapServerToClient()).ToList();
            return View("Users",list);

        }
        public ActionResult VerifyUser(int id)
        {

            if (_userRepository.VerifyUser(id))
            {
                ViewBag.verify = true;
            }
            else
            {
                ViewBag.verify = false;
            }
            
            List<UserProfileModel> list = _userRepository.GetAllProfiles().ToList().Select(x=>x.MapServerToClient()).ToList();
            return View("Users",list);

        }
        public ActionResult Disapprove(int id)
        {

            if (_userRepository.Disapprove(id))
            {
                ViewBag.notverify = true;
            }
            else
            {
                ViewBag.notverify = false;
            }
            
            List<UserProfileModel> list = _userRepository.GetAllProfiles().ToList().Select(x=>x.MapServerToClient()).ToList();
            return View("Users",list);

        }



    }
}
