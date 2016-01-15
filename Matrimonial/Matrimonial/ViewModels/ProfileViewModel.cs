using System.Collections.Generic;
using Matrimonial.ClientModels;
using Matrimonial.Models;

namespace Matrimonial.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            Heights = new List<Height>();
            Occupations = new List<Occupation>();
            UserProfile = new UserProfileModel();
            Requests = new List<Request>();
        }
        public List<Height> Heights { get; set; }
        public List<Occupation> Occupations { get; set; }
        public UserProfileModel UserProfile { get; set; }
        public string DateOfBirthString { get; set; }
        public List<Request> Requests { get; set; }
    }
}