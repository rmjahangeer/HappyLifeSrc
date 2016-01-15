using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Matrimonial.Models
{
    public class AdminRepository:IAdminRepositiry
    {
        MatrimonialEntities entities = new MatrimonialEntities();
        public bool IsAdmin(Admin admin)
        {
            Admin temp = entities.Admins.FirstOrDefault(x => x.Email.Equals(admin.Email) && x.Pasword.Equals(admin.Pasword));
            if(temp != null)
                return true;
            return false;
        }

        public Admin GetAdmin(string email)
        {
            Admin temp = entities.Admins.FirstOrDefault(x => x.Email.Equals(email));
            return temp;
        }
        public Admin GetAdmin(int id)
        {
            Admin temp = entities.Admins.FirstOrDefault(x => x.Id == id);
            return temp;
        }

        public bool EditProfile(Admin a)
        {

            try
            {
                Admin temp = entities.Admins.FirstOrDefault(x => x.Id == a.Id);
                if(!string.IsNullOrEmpty(a.Email))
                    temp.Email = a.Email;
                if(!string.IsNullOrEmpty(a.Name))
                    temp.Name = a.Name;
                if(!string.IsNullOrEmpty(a.Pasword))
                    temp.Pasword = a.Pasword;
                if(!string.IsNullOrEmpty(a.ProfileImage))
                    temp.ProfileImage = a.ProfileImage;

                entities.Entry(temp).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool ChangePassword(int id,string old, string newPassword)
        {
            Admin a = entities.Admins.FirstOrDefault(x => x.Id == id && x.Pasword.Equals(old));
            if (a == null)
            {
                return false;
            }
            a.Pasword = newPassword;
            entities.Entry(a).State = EntityState.Modified;
            entities.SaveChanges();
            return true;

        }
    }
}