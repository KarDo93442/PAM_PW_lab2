using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<Forecast> GetTodaysDescription(string cityKey);
        Task<Weather> GetYesterdayDescription(string cityKey);
        Task<NextForecast> GetNextHourDescription(string cityKey);
        Task<NextForecast> GetNext12HourDescription(string cityKey);
        Task<Forecast> GetNext5daysDesc(string cityKey);
    }
}
