using LeavePlanner.Utilities.Navigation;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Mvc;

namespace LeavePlanner.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;

            if (!userAuthenticated)
            {
                NavigationModel.NavigationJsonFile = "Navbar/nav.json";
            }
            else
            {
                if (User.IsInRole(UserRoles.Admin))
                    NavigationModel.NavigationJsonFile = "Navbar/admin.json";
                else if (User.IsInRole(UserRoles.Employee))
                    NavigationModel.NavigationJsonFile = "Navbar/employee.json";
                else
                {
                    NavigationModel.NavigationJsonFile = "Navbar/nav.json";
                }
            }

            var items = NavigationModel.Full;

            return View(items);
        }
    }
}