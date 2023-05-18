using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.HelpData.CustomHelpData
{
    public class IndexEmployeeHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel
            {
                Title = "Admin can list all application users",
                BasicInfoMessagesTitle = "Info:"
            };
            helpViewModel.BasicInfoMessages.Add("You can view details of each employee");
            helpViewModel.BasicInfoMessages.Add("You can edit each employee and assign user role 'Employee' or 'Admin'");

            return helpViewModel;
        }
    }
}
