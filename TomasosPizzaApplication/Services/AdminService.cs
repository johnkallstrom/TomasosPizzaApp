using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;

namespace TomasosPizzaApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToPremiumRole(ApplicationUser user)
        {
            var customer = _userService.FetchCurrentCustomer(user.Id);

            if (customer.BonusPoints == null)
            {
                customer.BonusPoints = 0;
            }

            await _userManager.RemoveFromRoleAsync(user, "RegularUser");
            var result = await _userManager.AddToRoleAsync(user, "PremiumUser");
            return result;
        }

        public async Task<IdentityResult> AddToRegularRole(ApplicationUser user)
        {
            await _userManager.RemoveFromRoleAsync(user, "PremiumUser");
            var result = await _userManager.AddToRoleAsync(user, "RegularUser");
            return result;
        }
    }
}
