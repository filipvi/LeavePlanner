using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.HelpData
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
