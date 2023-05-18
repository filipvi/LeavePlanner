using LeavePlanner.Models.ViewModels;

namespace LeavePlanner.Utilities.UserHelpData
{
    public abstract class HelpDataFactory
    {
        public static IHelpData GetHelpData(object callingObject)
        {
            return callingObject switch
            {
                LandPageViewModel => new LandPageViewModelHelpData(),
                _ => new EmptyHelpData()
            };
        }
    }
}