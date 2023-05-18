using LeavePlanner.Core.Interfaces;
using LeavePlanner.Utilities.Extensions;

namespace LeavePlanner.Models.Results
{
    public class CountSelectedWorkingDaysResult : Result
    {
        public int SelectedWorkingDays { get; set; }

        public async Task GetSelectedDaysCount(IHolidayService holidaysService, string dateFrom, string dateTo)
        {
            var holidays = await holidaysService.GetHolidaysForCountryAsync();
            var holidayDates = holidays.Select(x => x.Date).Distinct().ToList();

            DateTime startDate = dateFrom.StringToDateTime();
            DateTime endDate = dateTo.StringToDateTime();

            int countOfWorkingDays = 0;
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday && !holidayDates.Contains(currentDate))
                {
                    countOfWorkingDays++;
                }

                currentDate = currentDate.AddDays(1);
            }

            SelectedWorkingDays = countOfWorkingDays;
        }
    }
}
