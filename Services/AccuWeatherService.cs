using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public class AccuWeatherService : IAccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language{2}";
        private const string todays_description_endpoint = "forecasts/v1/daily/1day/{0}?apikey={1}&language{2}";
        private const string yesterday_description_endpoint = "/currentconditions/v1/{0}?apikey={1}&language{2}/historical/24";
        private const string next_hour_description_endpoint = "/forecasts/v1/hourly/1hour/{0}?apikey={1}&language{2}";
        private const string next_12h_description_endpoint = "/forecasts/v1/hourly/12hour/{0}?apikey={1}&language{2}";
        private const string next_5days_description_endpoint = "/forecasts/v1/daily/5day/{0}?apikey={1}&language{2}";

        // private const string api_key = "5hFl75dja3ZuKSLpXFxUzSc9vXdtnwG5";
        string api_key;
        //private const string language = "pl";
        string language;

        public AccuWeatherService()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json");

            var configuration = builder.Build();
            api_key = configuration["api_key"];
            language = configuration["default_language"];
        }


        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;

            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

        public async Task<Forecast> GetTodaysDescription(string cityKey)
        {
            string uri = base_url + "/" + string.Format(todays_description_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);
                return forecast;
            }
        }

        public async Task<Weather> GetYesterdayDescription(string cityKey)
        {
            string uri = base_url + "/" + string.Format(yesterday_description_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

        public async Task<NextForecast> GetNextHourDescription(string cityKey)
        {
            string uri = base_url + "/" + string.Format(next_hour_description_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                NextForecast[] nextForecasts = JsonConvert.DeserializeObject<NextForecast[]>(json);
                return nextForecasts.FirstOrDefault();
            }
        }

        public async Task<NextForecast> GetNext12HourDescription(string cityKey)
        {
            string uri = base_url + "/" + string.Format(next_12h_description_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                NextForecast[] nextForecasts = JsonConvert.DeserializeObject<NextForecast[]>(json);
                return nextForecasts.FirstOrDefault();
            }
        }

        public async Task<Forecast> GetNext5daysDesc(string cityKey)
        {
            string uri = base_url + "/" + string.Format(next_5days_description_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);
                return forecast;
            }
        }

    }
}
