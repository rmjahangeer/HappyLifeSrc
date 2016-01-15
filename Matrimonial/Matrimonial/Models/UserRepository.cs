using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Matrimonial.Models
{
    public class UserRepository : IUserRepository
    {
        MatrimonialEntities entity = new MatrimonialEntities();
        public bool AddNew(UserProfile user)
        {
            try
            {
                entity.UserProfiles.Add(user);
                entity.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<UserProfile> GetAllProfiles()
        {
            List<UserProfile> list;
            try
            {
                list = entity.UserProfiles.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            return list;

        }

        public List<UserProfile> GetFilteredProfiles(Search search)
        {
            List<UserProfile> list;
            try
            {
                list =
                        entity.UserProfiles.Where(
                            x => (x.Gender.Equals(search.Gender) && 
                                ((search.AgeFrom == 0 || (x.Age >= search.AgeFrom)) && (search.AgeTo == 0 || x.Age <= search.AgeTo)) && 
                                (string.IsNullOrEmpty(search.City) || search.City==x.City) && 
                                (string.IsNullOrEmpty(search.MaritalStatus)  || x.MaritalStatus.Equals(search.MaritalStatus))))
                            .Select(x => x)
                            .ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            return list;
        }

        public bool IsUser(UserProfile user)
        {
            UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
            if (u == null)
                return false;
            return true;
        }

        public UserProfile GetUser(string email)
        {
            UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.Email.Equals(email));
            return u;
        }

        public void AddProfilePicture(string img, int id)
        {
            UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == id);
            u.ProfileImage = img;
            entity.Entry(u).State = EntityState.Modified;
            entity.SaveChanges();
        }

        public bool UpdateProfile(UserProfile user)
        {
            try
            {
                entity.UserProfiles.AddOrUpdate(user);
                entity.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            return true;
        }

        public List<UserProfile> GetProposals(int id)
        {
            UserProfile temp = entity.UserProfiles.FirstOrDefault(x => x.UserId == id);

            var q = entity.Requests.Where(x => x.UserID == id).Select(x => x).ToList();
            List<UserProfile> myList = new List<UserProfile>();
            foreach (Request request in q)
            {
                UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == request.RequestedProfileId);
                myList.Add(u);
            }
            return myList;
        }

        public List<UserProfile> GetShortListProfiles(int id)
        {
            var q = entity.ShortLists.Where(x => x.UserId == id).Select(x => x).ToList();
            List<UserProfile> myList = new List<UserProfile>();
            foreach (ShortList shortList in q)
            {
                UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == shortList.ShortlistProfileId);
                myList.Add(u);
            }
            return myList;
        }

        public void AcceptRequest(int requestId, int userId)
        {
            Request r = entity.Requests.FirstOrDefault(x => x.RequestedProfileId == requestId && x.UserID == userId);
            entity.Requests.Remove(r);
            entity.SaveChanges();
            ShortList s = new ShortList()
            {
                UserId = userId,
                ShortlistProfileId = requestId
            };
            entity.ShortLists.Add(s);
            entity.SaveChanges();
        }

        public void DeleteRequest(int requestId, int userId)
        {
            Request r = entity.Requests.FirstOrDefault(x => x.RequestedProfileId == requestId && x.UserID == userId);
            entity.Requests.Remove(r);
            entity.SaveChanges();
        }

        public bool DeleteUser(int id)
        {
            try
            {
                UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == id);
                var q = entity.Requests.Where(x => x.Id == id);
                foreach (var request in q)
                {
                    entity.Requests.Remove(request);
                }
                entity.UserProfiles.Remove(u);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool VerifyUser(int id)
        {
            try
            {
                UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == id);
                u.isVerified = "true";
                //entity.Entry(u).State = EntityState.Modified;
                entity.UserProfiles.AddOrUpdate(u);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Disapprove(int id)
        {
            try
            {
                UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.UserId == id);
                u.isVerified = "false";
                //entity.Entry(u).State = EntityState.Modified;
                entity.UserProfiles.AddOrUpdate(u);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SendRequest(int id, int requestedId)
        {
            try
            {
                Request r = new Request();
                r.UserID = id;
                r.RequestedProfileId = requestedId;
                entity.Requests.Add(r);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RemoveShortList(int requestId, int userId)
        {
            ShortList r = entity.ShortLists.FirstOrDefault(x => x.ShortlistProfileId == requestId && x.UserId == userId);
            entity.ShortLists.Remove(r);
            entity.SaveChanges();
        }
        public bool IsAvailable(string email)
        {
            UserProfile u = entity.UserProfiles.FirstOrDefault(x => x.Email.Equals(email));
            if(u!=null)
                return true;
            return false;
        }

    }
}