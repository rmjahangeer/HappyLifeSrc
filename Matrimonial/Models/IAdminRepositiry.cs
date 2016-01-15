using System.Web.Mvc;

namespace Matrimonial.Models
{
    interface IAdminRepositiry
    {
        bool IsAdmin(Admin admin);

        Admin GetAdmin(string email);
        Admin GetAdmin(int id);

        bool EditProfile(Admin a);
        bool ChangePassword(int id,string old, string newPassword);
    }
}
