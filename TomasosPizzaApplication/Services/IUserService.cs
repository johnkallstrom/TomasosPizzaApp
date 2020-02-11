using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> FetchUser();
        Task<IdentityResult> CreateUser(Kund customer, string username, string password);
        Task<SignInResult> SignInUser(string username, string password);
        Task<bool> SignOutUser();
    }
}
