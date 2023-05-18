
using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.UserHelpData
{
    public class EmptyHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel();

            return helpViewModel;
        }
    }
}
