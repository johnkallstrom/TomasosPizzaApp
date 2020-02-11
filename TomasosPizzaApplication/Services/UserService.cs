using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<bool> CheckPassword(ApplicationUser user, string currentPassword)
        {
            return await _userManager.CheckPasswordAsync(user, currentPassword);
        }

        public async Task<IdentityResult> CreateUser(Kund customer, string username, string password)
        {
            var user = new ApplicationUser { UserName = username, Email = customer.Email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                customer.UserId = user.Id;
                _userRepository.Add(customer);
            }

            return result;
        }

        public Kund FetchCurrentCustomer(string id)
        {
            return _userRepository.Fetch(id);
        }

        public async Task<ApplicationUser> FetchCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

        public async Task<SignInResult> SignInUser(string username, string password)
        {
            return await _signInManager
                .PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<bool> SignOutUser()
        {
            await _signInManager.SignOutAsync();
            _httpContextAccessor.HttpContext.Session.Clear();
            return true;
        }

        public void UpdateUserDetails(ApplicationUser user, Kund kund)
        {
            kund.UserId = user.Id;
            _userRepository.Update(kund);
        }

        public async Task<bool> UpdateUsername(ApplicationUser user, string currentPassword, string updatedUsername)
        {
            var isPassValid = await CheckPassword(user, currentPassword);

            if (isPassValid == true)
            {
                user.UserName = updatedUsername;
                await _userManager.UpdateAsync(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdatePassword(ApplicationUser user, Kund customer, string currentPassword, string newPassword)
        {
            var isPassValid = await CheckPassword(user, currentPassword);

            if (isPassValid == true)
            {
                customer.UserId = user.Id;
                await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
