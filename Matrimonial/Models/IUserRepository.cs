using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimonial.Models
{
    interface IUserRepository
    {
        bool AddNew(UserProfile user);

        List<UserProfile> GetAllProfiles();

        List<UserProfile> GetFilteredProfiles(Search search);

        bool IsUser(UserProfile user);

        bool IsAvailable(string email);
        UserProfile GetUser(string email);

        void AddProfilePicture(string img, int id);

        bool UpdateProfile(UserProfile user);
        List<UserProfile> GetProposals(int id);
        List<UserProfile> GetShortListProfiles(int id);
        void AcceptRequest(int requestId, int userId);
        void DeleteRequest(int requestId, int userId);
        bool DeleteUser(int id);
        bool VerifyUser(int id);
        bool Disapprove(int id);
        bool SendRequest(int id, int requestedId);
        void RemoveShortList(int requestId, int userId);
    }

    public class Search
    {
        public string Gender { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public string City { get; set; }
        public string MaritalStatus { get; set; }
    }
}
