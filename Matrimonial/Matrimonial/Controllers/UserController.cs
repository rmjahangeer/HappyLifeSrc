using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Matrimonial.ClientModels;
using Matrimonial.Mappers;
using Matrimonial.Models;
using Matrimonial.ViewModels;

namespace Matrimonial.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        IUserRepository _entityUser = new UserRepository();
        ISiteData _entityData = new SiteDataRepository();
        MatrimonialEntities e = new MatrimonialEntities();
        public ActionResult ViewProfile()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
            
            var temp = (UserProfileModel) Session["user"];
            UserProfile u = _entityUser.GetUser(temp.Email);
            ViewBag.height = e.Heights.FirstOrDefault(x => x.Id == u.Height);
            ViewBag.occupation = e.Occupations.FirstOrDefault(x => x.Id == u.Occupation);
            return View(temp);
        }
        public ActionResult EditProfile()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            profileViewModel.Heights = GetAllHeights();

            profileViewModel.Occupations = GetAllOccupations();
            var temp = (UserProfileModel) Session["user"];
            profileViewModel.UserProfile = _entityUser.GetUser(temp.Email).MapServerToClient();
            profileViewModel.DateOfBirthString = profileViewModel.UserProfile.DateOfBirth.ToShortDateString();
           
            return View(profileViewModel);
        }
        public ActionResult UploadImage(HttpPostedFileBase file)
        {

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            var u = (UserProfileModel) Session["user"];

            String temp = Server.MapPath("~/UserUploads/"+u.Email+"/");
            try
            {
                // Determine whether the directory exists. 
                if (!Directory.Exists(temp))
                {
                    DirectoryInfo di = Directory.CreateDirectory(temp);
                    
                }
            }
            catch (Exception error)
            {
                return View("Error",error.Message);
            }
            if (file != null && file.ContentLength > 0)
            {
                //var fileName = Path.GetFileName(file.FileName);
                try
                {
                    var path = Server.MapPath("~/UserUploads/"+ u.Email+"/" + file.FileName);
                    file.SaveAs(path);
                    _entityUser.AddProfilePicture(file.FileName, u.UserId);
                }
                catch (Exception error)
                {
                    return View("Error",error.Message);
                }
            }
            u = _entityUser.GetUser(u.Email).MapServerToClient();
            Session["user"] = u;
            return RedirectToAction("EditProfile", Session["user"]);
        }
        
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel user)
        {
            var temp = (UserProfileModel) Session["user"];
            
            if (_entityUser.UpdateProfile(user.UserProfile.MapClientToServer()))
            {
                ViewBag.changed = true;
                temp = _entityUser.GetUser(temp.Email).MapServerToClient();
                Session["user"] = temp;
                return RedirectToAction("EditProfile", Session["user"]);
            }
            return View("Error");
        }
        //public JsonResult SearchResults()
        //{
        //    string city = Request["city"];
        //    int age = int.Parse(Request["age"]);
        //    string gender = Request["gender"];
        //    string maritalStatus = Request["maritalStatus"];

        //    int fromAge = 0;
        //    int toAge = 0;

        //    if (age == 0)
        //    {
        //        fromAge = 0;
        //        toAge = 200;
        //    }
        //    if (age == 1)
        //    {
        //        fromAge = 18;
        //        toAge = 25;
        //    }
        //    if (age == 2)
        //    {
        //        fromAge = 26;
        //        toAge = 30;
        //    }
        //    if (age == 3)
        //    {
        //        fromAge = 31;
        //        toAge = 40;
        //    }
        //    if (age == 4)
        //    {
        //        fromAge = 41;
        //        toAge = 200;
        //    }

        //    List<UserProfile> list;
        //    Search s = new Search();
        //    s.AgeFrom = fromAge;
        //    s.AgeTo = toAge;
        //    s.City = city;
        //    s.Gender = gender;
        //    s.MaritalStatus = maritalStatus;

        //    list = _entityUser.GetFilteredProfiles(s);

        //    if (list.Count == 0)
        //        return Json(true, JsonRequestBehavior.AllowGet);

        //    List<Matrimonial.ClientModels.UserProfile> temp = new List<Matrimonial.ClientModels.UserProfile>();
        //    foreach (UserProfile userProfile in list)
        //    {
        //        ClientModels.UserProfile p = new ClientModels.UserProfile()
        //        {
        //            Age = userProfile.Age,
        //        City = userProfile.City,
        //        MaritalStatus = userProfile.MaritalStatus,
        //        Email = userProfile.Email,
        //        FirstName = userProfile.FirstName,
        //        Gender = userProfile.Gender,
        //        LastName = userProfile.LastName,
        //        UserId = userProfile.UserId
        //        };
        //        temp.Add(p);
        //    }
        //    return Json(temp, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SearchResults()
        {
            string city = Request["city"];
            int age = int.Parse(Request["age"]);
            string gender = Request["gender"];
            string maritalStatus = Request["maritalStatus"];
            if (maritalStatus.Equals("Never"))
                maritalStatus = "Never Married";
            int fromAge = 0;
            int toAge = 0;

            if (age == 0)
            {
                fromAge = 0;
                toAge = 200;
            }
            if (age == 1)
            {
                fromAge = 18;
                toAge = 25;
            }
            if (age == 2)
            {
                fromAge = 26;
                toAge = 30;
            }
            if (age == 3)
            {
                fromAge = 31;
                toAge = 40;
            }
            if (age == 4)
            {
                fromAge = 41;
                toAge = 200;
            }

            List<UserProfileModel> list;
            Search s = new Search();
            s.AgeFrom = fromAge;
            s.AgeTo = toAge;
            s.City = city;
            s.Gender = gender;
            s.MaritalStatus = maritalStatus;

            list = _entityUser.GetFilteredProfiles(s).Select(x=>x.MapServerToClient()).ToList();

            return View("_SearchResults",list);
        }

        public JsonResult IsUser()
        {
            string email = Request["email"];
            if (_entityUser.IsAvailable(email))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShortLists()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
            try
            {
                var u = (UserProfileModel) Session["user"];
                List<UserProfileModel> list = _entityUser.GetShortListProfiles(u.UserId).Select(x=>x.MapServerToClient()).ToList();
                if (list.Count == 0)
                    ViewBag.results = false;
                return View(list);
            }
            catch (Exception)
            {
                ViewBag.results = false;
                return View();
            }
        }
        public ActionResult Proposals()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
            try
            {
                var u = (UserProfileModel)Session["user"];
                List<UserProfileModel> list = _entityUser.GetProposals(u.UserId).Select(x=>x.MapServerToClient()).ToList();
                if(list.Count == 0)
                    ViewBag.results = false;
                return View(list);
            }
            catch (Exception)
            {
                ViewBag.results = false;
                return View();
            }
        }

        //Helper Methods
        private List<Height> GetAllHeights()
        {
            List<Height> list = _entityData.GetAllHeights();
            //List<SelectListItem> listItems = new List<SelectListItem>();
            //int i = 0;
            //foreach (Height height in list)
            //{
            //    listItems.Add(new SelectListItem(){Text = height.Height1, Value = height.Id.ToString()});
            //    i++;
            //}
            return list;

        }
        private List<Occupation> GetAllOccupations()
        {
            List<Occupation> list = _entityData.GetAllOccupation();
            //List<SelectListItem> listItems = new List<SelectListItem>();
            //int i = 0;
            //foreach (Occupation occupation in list)
            //{
            //    listItems.Add(new SelectListItem(){Text = occupation.Occupation1, Value = occupation.Id.ToString()});
            //    i++;
            //}
            return list;

        }

        public ActionResult Search()
        {
            var model = _entityUser.GetAllProfiles().ToList().Select(x => x.MapServerToClient()).ToList();
            return View(model);
        }

        public ActionResult AcceptRequest(int id)
        {
            try
            {
                var u = (UserProfileModel) Session["user"];
                int userId = u.UserId;
                _entityUser.AcceptRequest(id,userId);
            }
            catch (Exception error)
            {
                return View("Error");
            }

            return RedirectToAction("Proposals");
        }
        public ActionResult DeleteRequest(int id)
        {
            try
            {
                var u = (UserProfileModel) Session["user"];
                int userId = u.UserId;
                _entityUser.DeleteRequest(id,userId);
            }
            catch (Exception error)
            {
                return View("Error");
            }

            return RedirectToAction("Proposals");
        }
        public ActionResult RemoveFromShortList(int id)
        {
            try
            {
                var u = (UserProfileModel) Session["user"];
                int userId = u.UserId;
                _entityUser.RemoveShortList(id,userId);
            }
            catch (Exception error)
            {
                return View("Error");
            }

            return RedirectToAction("ShortLists");
        }


        public ActionResult SendRequest(int userId, int requestedId)
        {
            if (_entityUser.SendRequest(userId, requestedId))
                ViewBag.accept = true;
            else
                ViewBag.accept = false;

            return RedirectToAction("Search");
        }
    }
}
