using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.HelpData.CustomHelpData
{
    public class EditEmployeeHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel
            {
                Title = "Edit employee data",
                BasicInfoMessagesTitle = "Info:"
            };
            helpViewModel.BasicInfoMessages.Add("You can edit number of leave days per year");
            helpViewModel.BasicInfoMessages.Add("You can manage user role");

            return helpViewModel;
        }
    }
}
