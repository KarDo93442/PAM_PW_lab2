using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P04WeatherForecastAPI.Client.Models;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class NextForecastViewModel
    {
        public string iconPhrase { get; set; }

        public NextForecastViewModel(NextForecast nextForecast)
        {
            iconPhrase = nextForecast.IconPhrase;
        }
    }
}
