
using LeavePlanner.Utilities.Navigation;
using Microsoft.AspNetCore.Mvc;

namespace LeavePlanner.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            NavigationModel.NavigationJsonFile = "Navbar/nav.json";

            //var userAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;

            var items = NavigationModel.Full;

            return View(items);
        }
    }
}
