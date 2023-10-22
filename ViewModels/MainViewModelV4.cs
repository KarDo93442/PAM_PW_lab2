using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModelV4 : ObservableObject
    {
        private Weather _weather;
        private Forecast _todayForecast;
        private Weather _yesterdayWeather;
        private NextForecast _nextHour;
        private NextForecast _next12Hours;
        private Forecast _next5days;

        private CityViewModel _selectedCity;
        private readonly IAccuWeatherService _accuWeatherService;


        public MainViewModelV4(IAccuWeatherService accuWeatherService)
        {
            // _serviceProvider= serviceProvider; 
            //LoadCitiesCommand = new RelayCommand(x => LoadCities(x as string));
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>(); // podejście nr 2 
        }

        [ObservableProperty]
        public WeatherViewModel weatherView;


        [ObservableProperty]
        public ForecastViewModel todayForecastView;

        [ObservableProperty]
        public WeatherViewModel yesterdayForecastView;

        [ObservableProperty]
        public NextForecastViewModel nextHourView;

        [ObservableProperty]
        public NextForecastViewModel next12HourView;

        [ObservableProperty]
        public ForecastViewModel next5dayView;



        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }

         
        private async void LoadWeather()
        {
            if(SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key); 
                WeatherView = new WeatherViewModel(_weather);

                _todayForecast = await _accuWeatherService.GetTodaysDescription(SelectedCity.Key);
                TodayForecastView = new ForecastViewModel(_todayForecast);

                _yesterdayWeather = await _accuWeatherService.GetYesterdayDescription(SelectedCity.Key);
                YesterdayForecastView = new WeatherViewModel(_yesterdayWeather);

                _nextHour = await _accuWeatherService.GetNextHourDescription(SelectedCity.Key);
                NextHourView = new NextForecastViewModel(_nextHour);

                _next12Hours = await _accuWeatherService.GetNext12HourDescription(SelectedCity.Key);
                Next12HourView = new NextForecastViewModel(_next12Hours);

                _next5days = await _accuWeatherService.GetNext5daysDesc(SelectedCity.Key);
                Next5dayView = new ForecastViewModel(_next5days);
            }
        } 

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities) 
                Cities.Add(new CityViewModel(city));
        }


    }
}
