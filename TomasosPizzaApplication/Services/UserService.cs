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
        private readonly IUserRepository _repository;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<IdentityResult> CreateUser(Kund customer, string username, string password)
        {
            var user = new ApplicationUser { UserName = username, Email = customer.Email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                customer.UserId = user.Id;
                _repository.AddUser(customer);
            }

            return result;
        }

        public async Task<ApplicationUser> FetchUser()
        {
            var user = await _userManager.GetUserAsync(User);
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
    }
}
