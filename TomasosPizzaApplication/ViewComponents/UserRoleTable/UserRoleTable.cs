using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasosPizzaApplication.IdentityData;

namespace TomasosPizzaApplication.ViewComponents
{
    public class UserRoleTable : ViewComponent
    {
        public IViewComponentResult Invoke(List<ApplicationUser> model)
        {
            return View("UserRoleTable", model);
        }
    }
}
