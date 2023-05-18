using LeavePlanner.Utilities.UserHelpData;

namespace LeavePlanner.Models.ViewModels
{
    public class LandPageViewModel
    {
        public HelpViewModel HelpModel { get; set; }

        public void PrepareHelpData()
        {
            var helpData = HelpDataFactory.GetHelpData(this);

            HelpModel = helpData.GetHelpData();
        }
    }
}