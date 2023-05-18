using LeavePlanner.Models.DTOs;

namespace LeavePlanner.Core.Interfaces;

public interface IHolidayService
{
    Task<List<DateTime>> GetHolidaysDatesForCountryAsync();
    Task<List<HolidayDto>> GetHolidaysForCountryAsync();

}