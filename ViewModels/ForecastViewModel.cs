using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class ForecastViewModel
    {
        public string text { get; set; }

        public ForecastViewModel(Forecast forecast)
        {
            text = forecast.headline.text;
        }


    }
}
