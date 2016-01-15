using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Matrimonial.Models
{
    public class MetaDataUserProfile
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name required")]
        [Display(Name = "First Name")]
        [MaxLength(20,ErrorMessage = "Max Length not More than 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "Max Length not More than 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Specify your gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }

        //[DataType(DataType.Upload)]
        public string ProfileImage { get; set; }

        [DataType(DataType.Currency)]
        public string AnualIncome { get; set; }
        public Nullable<int> Occupation { get; set; }
        public string Employed { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string AboutUser { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string isVerified { get; set; }
        public Nullable<int> Height { get; set; }
        public string MaritalStatus { get; set; }
        public string Complexion { get; set; }
        public string BodyType { get; set; }
        public string PersonalValues { get; set; }
        public string ResidencyStatus { get; set; }
        public string WorkingWith { get; set; }
        public string ContactDetails { get; set; }
    
        public virtual Height Height1 { get; set; }
        public virtual Occupation Occupation1 { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<ShortList> ShortLists { get; set; }
    }
}