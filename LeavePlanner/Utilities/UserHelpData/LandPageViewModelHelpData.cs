using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.UserHelpData
{
    public class LandPageViewModelHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel();
            helpViewModel.Title = "Pomoć";

            helpViewModel.BasicInfoMessagesTitle = "Info:";
            helpViewModel.BasicInfoMessages.Add("Ovdje se navodi pomoć korisniku za ekran na kojemu se nalazi");

            return helpViewModel;

        }
    }
}