using LeavePlanner.Core.Interfaces;
using LeavePlanner.Models.DTOs;
using LeavePlanner.Utilities.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LeavePlanner.Persistence.Repositories;

public class HolidayService : IHolidayService
{
    private readonly IOptions<HolidayApi> _holidayApi;

    public HolidayService(IOptions<HolidayApi> holidayApi)
    {
        _holidayApi = holidayApi;
    }

    public async Task<List<HolidayDto>> GetHolidaysForCountryAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"{_holidayApi.Value.NextHolidaysForCountryUrl}/{_holidayApi.Value.CountryCode}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                await using var jsonStream = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<HolidayDto>>(jsonStream, jsonSerializerOptions);
            }

            throw new Exception("Error fetching holidays;");
        }
    }

    public async Task<List<DateTime>> GetHolidaysDatesForCountryAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"{_holidayApi.Value.NextHolidaysForCountryUrl}/{_holidayApi.Value.CountryCode}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                await using var jsonStream = await response.Content.ReadAsStreamAsync();
                var holidays = JsonSerializer.Deserialize<List<HolidayDto>>(jsonStream, jsonSerializerOptions);
                return holidays.Select(x => x.Date).Distinct().ToList();
            }

            throw new Exception("Error fetching holidays;");
        }
    }
}