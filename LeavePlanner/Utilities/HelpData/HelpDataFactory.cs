using LeavePlanner.Models.ViewModels.Employee;
using LeavePlanner.Models.ViewModels.Leave;
using LeavePlanner.Utilities.HelpData.CustomHelpData;

namespace LeavePlanner.Utilities.HelpData
{
    public class HelpDataFactory
    {
        public static IHelpData GetHelpData(object callingObject)
        {
            return callingObject switch
            {
                IndexEmployeeViewModel => new IndexEmployeeHelpData(),
                DetailsEmployeeViewModel => new DetailsEmployeeHelpData(),
                EditEmployeeViewModel => new EditEmployeeHelpData(),
                IndexLeaveViewModel => new IndexLeaveHelpData(),
                _ => new EmptyHelpData()
            };
        }
    }
}
