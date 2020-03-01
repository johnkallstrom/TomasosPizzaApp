using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class UserService : IUserService
    {
        private readonly ISessionService _sessionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ICartService _cartService;

        public UserService(
            ICartService cartService,
            ISessionService sessionService,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _cartService = cartService;
            _sessionService = sessionService;
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
                await _userManager.AddToRoleAsync(user, "RegularUser");
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
            _sessionService.Clear();
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

        public List<ApplicationUser> FetchAllUsers()
        {
            return _userManager.Users
                .OrderByDescending(x => x.UserName).ToList();
        }

        public async Task<ApplicationUser> FetchUserByID(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> IsUserRegular(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "RegularUser");
        }

        public async Task<bool> IsUserPremium(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "PremiumUser");
        }

        public async Task<bool> IsUserAdmin(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<bool> AddBonusPointsToUser(Kund kund, int bonusPoints)
        {
            var succeeded = false;
            var user = await FetchCurrentUser();

            if (await IsUserPremium(user) == true)
            {
                kund.BonusPoints += bonusPoints;
                kund.UserId = user.Id;
                _userRepository.Update(kund);
                succeeded = true;
                return succeeded;
            }

            return succeeded;
        }

        public async Task<bool> RemoveBonusPointsFromUser(Kund kund)
        {
            var succeeded = false;
            var user = await FetchCurrentUser();

            if (await IsUserPremium(user) == true && kund.BonusPoints >= 100)
            {
                kund.BonusPoints -= 100;
                kund.UserId = user.Id;
                _userRepository.Update(kund);
                succeeded = true;
                return succeeded;
            }

            return succeeded;
        }
    }
}
