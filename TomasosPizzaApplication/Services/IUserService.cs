using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IUserService
    {
        Task<bool> IsUserPremium(ApplicationUser user);
        Task<bool> IsUserRegular(ApplicationUser user);
        Task<ApplicationUser> FetchUserByID(string id);
        List<ApplicationUser> FetchAllUsers();
        Task<bool> UpdateUsername(ApplicationUser user, string currentPassword, string updatedUsername);
        Task<bool> CheckPassword(ApplicationUser user, string currentPassword);
        Task<bool> UpdatePassword(ApplicationUser user, Kund customer, string currentPassword, string newPassword);
        void UpdateUserDetails(ApplicationUser user, Kund kund);
        Kund FetchCurrentCustomer(string id);
        Task<ApplicationUser> FetchCurrentUser();
        Task<IdentityResult> CreateUser(Kund customer, string username, string password);
        Task<SignInResult> SignInUser(string username, string password);
        Task<bool> SignOutUser();
    }
}
