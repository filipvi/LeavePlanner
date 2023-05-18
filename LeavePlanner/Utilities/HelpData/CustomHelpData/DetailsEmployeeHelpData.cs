using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.HelpData.CustomHelpData
{
    public class DetailsEmployeeHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel
            {
                Title = "Employee details",
                BasicInfoMessagesTitle = "Info:"
            };
            helpViewModel.BasicInfoMessages.Add(
                "Displayed basic informations and leave requests for employee");

            HelpViewModel.TabItem edit = new HelpViewModel.TabItem
            {
                MessageTitle = "Edit employee"
            };
            edit.Messages.Add("You can edit available employees data");
            helpViewModel.TabItems.Add(edit);

            HelpViewModel.TabItem delete = new HelpViewModel.TabItem
            {
                MessageTitle = "Delete employee"
            };
            delete.Messages.Add("You can delete employee and all related data");
            helpViewModel.TabItems.Add(delete);

            return helpViewModel;
        }
    }
}
