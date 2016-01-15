using Matrimonial.ClientModels;

namespace Matrimonial.Mappers
{
    public static class UserProfileMapper
    {
        public static UserProfileModel MapServerToClient(this Models.UserProfile source)
        {
            return new UserProfileModel
            {
                UserId = source.UserId,
                Age = source.Age,
                City = source.City,
                Email = source.Email,
                FirstName = source.FirstName,
                Gender = source.Gender,
                LastName = source.LastName,
                MaritalStatus = source.MaritalStatus,
                AboutUser = source.AboutUser,
                Address = source.Address,
                AnualIncome = source.AnualIncome,
                BodyType = source.BodyType,
                Complexion = source.Complexion,
                ContactDetails = source.ContactDetails,
                Country = source.Country,
                DateOfBirth = source.DateOfBirth,
                Employed = source.Employed,
                Height = source.Height,
                IsVerified = source.isVerified,
                Occupation = source.Occupation,
                PersonalValues = source.PersonalValues,
                Phone = source.Phone,
                ProfileImage = source.ProfileImage,
                Religion = source.Religion,
                ResidencyStatus = source.ResidencyStatus,
                WorkingWith = source.WorkingWith,
                Password = source.Password
            };
        }

        public static Models.UserProfile MapClientToServer(this UserProfileModel source)
        {
            return new Models.UserProfile
            {
                UserId = source.UserId,
                Age = source.Age,
                City = source.City,
                Email = source.Email,
                FirstName = source.FirstName,
                Gender = source.Gender,
                LastName = source.LastName,
                MaritalStatus = source.MaritalStatus,
                AboutUser = source.AboutUser,
                Address = source.Address,
                AnualIncome = source.AnualIncome,
                BodyType = source.BodyType,
                Complexion = source.Complexion,
                ContactDetails = source.ContactDetails,
                Country = source.Country,
                DateOfBirth = source.DateOfBirth,
                Employed = source.Employed,
                Height = source.Height,
                isVerified = source.IsVerified,
                Occupation = source.Occupation,
                PersonalValues = source.PersonalValues,
                Phone = source.Phone,
                ProfileImage = source.ProfileImage,
                Religion = source.Religion,
                ResidencyStatus = source.ResidencyStatus,
                WorkingWith = source.WorkingWith,
                Password = source.Password

            };
        }
    }
}