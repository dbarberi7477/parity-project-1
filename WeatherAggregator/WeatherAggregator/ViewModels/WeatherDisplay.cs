using System.ComponentModel.DataAnnotations;

namespace WeatherAggregator.ViewModels
{
	public class WeatherDisplay
	{
		public int id { get; set; }

		[Display(Name = "Region")]
		public string region { get; set; }

		[Display(Name = "Temperature (F)")]
		public decimal? temperature { get; set; }

		[Display(Name = "Pressure (mbar)")]
		public decimal? pressure { get; set; }

		[Display(Name = "Humidity (%)")]
		public decimal? humidity { get; set; }

		[Display(Name = "Wind Speed (MPH)")]
		public decimal? wind_speed { get; set; }
	}
}