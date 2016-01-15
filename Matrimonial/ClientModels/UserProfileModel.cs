using System;

namespace Matrimonial.ClientModels
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Religion { get; set; }
        public string ProfileImage { get; set; }
        public string AnualIncome { get; set; }
        public int? Occupation { get; set; }
        public string Employed { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string AboutUser { get; set; }
        public string Phone { get; set; }
        public string IsVerified { get; set; }
        public int? Height { get; set; }
        public string MaritalStatus { get; set; }
        public string Complexion { get; set; }
        public string BodyType { get; set; }
        public string PersonalValues { get; set; }
        public string ResidencyStatus { get; set; }
        public string WorkingWith { get; set; }
        public string ContactDetails { get; set; }
        public int? Age { get; set; }
        
    }
}