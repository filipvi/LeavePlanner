using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.HelpData.CustomHelpData
{
    public class IndexLeaveHelpData : IHelpData
    {
        public HelpViewModel GetHelpData()
        {
            var helpViewModel = new HelpViewModel
            {
                Title = "Calendar with employees leaves",
                BasicInfoMessagesTitle = "Info:"
            };
            helpViewModel.BasicInfoMessages.Add("Past leaves are marked in muted and you cannot manage those");
            helpViewModel.BasicInfoMessages.Add("Based on your permissions you can manage leaves");
            helpViewModel.BasicInfoMessages.Add("You can select day/days in future to create new leave request");
            helpViewModel.BasicInfoMessages.Add("Admin cannot create leave requests");
            helpViewModel.BasicInfoMessages.Add("Employee must have user role 'Employee' to create leave request");
            helpViewModel.BasicInfoMessages.Add("Employee must choose replacement employee while creating leave request");

            return helpViewModel;
        }
    }
}
