using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using WeatherAggregator.Models;
using WeatherAggregator.ViewModels;

namespace WeatherAggregator.Repositories
{
	public class WeatherRepo
	{
		public PFAssessmentEntities db = new PFAssessmentEntities(); // Declare global connection to database entities

		public int LoadRawData(int region_id)
		{
			int status = 1; // Flag to indicate success/failure for AJAX call

			try
			{
				using (var client = new HttpClient()) // Declare object to connect to weather API
				{
					client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");

					// Load all locations for selected region
					var qGetRegion = (
						from region in db.region
						join location in db.location on region.region_id equals location.region_id
						where region.region_id == region_id
						select new
						{
							location.location_id,
							location.description,
							location.lat,
							location.lng
						}).ToList();

					foreach (var locn in qGetRegion)
					{
						// Update connection object to current location and acquire data from API
						var responseTask = client.GetAsync("weather?lat=" + locn.lat + "&lon=" + locn.lng + "&units=imperial&appid=9e1682bbea28fc8a3465ba65eefe75a3");
						responseTask.Wait();

						// Declare object to store data from API call
						Classes.WeatherClasses.Root weather_data = null;

						var result = responseTask.Result;
						if (result.IsSuccessStatusCode)
						{
							// Read API string
							var readTask = result.Content.ReadAsStringAsync();
							readTask.Wait();

							// Convert API string to object
							weather_data = JsonConvert.DeserializeObject<Classes.WeatherClasses.Root>(readTask.Result);
						}

						if (weather_data != null)
						{
							// Declare new raw data object for insertion into database with location weather data
							raw new_raw_data = new raw
							{
								location_id = locn.location_id,
								collection_date = DateTime.Now,
								temperature = (decimal)weather_data.main.temp,
								pressure = weather_data.main.pressure,
								humidity = weather_data.main.humidity,
								wind_speed = (decimal)weather_data.wind.speed
							};

							// Insert data into database
							DbSet<raw> new_raw_insert = db.Set<raw>();
							new_raw_insert.Add(new_raw_data);
							db.SaveChanges();
						}
					}
				}

				// Aggregate with newly acquired regional data to update display
				AggregateWeatherData(region_id);
			}
			catch
			{
				status = 0;
			}

			return status;
		}

		// Load from raw data tables for specified region, calculate average values for indicated columns, and update averages table with new values
		public void AggregateWeatherData(int region_id)
		{
			// Acquire raw data for indicated region from database
			var qGetWeatherData = (
				from region in db.region
				join location in db.location on region.region_id equals location.region_id
				join raw in db.raw on location.location_id equals raw.location_id
				where region.region_id == region_id
				select new
				{
					raw.temperature,
					raw.pressure,
					raw.humidity,
					raw.wind_speed
				}).ToList();

			// Calculate averages from results, filtered by column
			var avg_temperature = qGetWeatherData.Select(s => s.temperature).Average();
			var avg_pressure = qGetWeatherData.Select(s => s.pressure).Average();
			var avg_humidity = qGetWeatherData.Select(s => s.humidity).Average();
			var avg_wind_speed = qGetWeatherData.Select(s => s.wind_speed).Average();

			var record = db.averages.SingleOrDefault(w => w.region_id == region_id); // Find existing region data if exists
			if (record != null) // Update record if exists
			{
				record.temperature = avg_temperature;
				record.pressure = avg_pressure;
				record.humidity = avg_humidity;
				record.wind_speed = avg_wind_speed;

				db.SaveChanges();
			}
			else // Insert new record if does not exist
			{
				averages new_averages_data = new averages
				{
					region_id = region_id,
					temperature = avg_temperature,
					pressure = avg_pressure,
					humidity = avg_humidity,
					wind_speed = avg_wind_speed
				};

				DbSet<averages> new_averages_insert = db.Set<averages>();
				new_averages_insert.Add(new_averages_data);
				db.SaveChanges();
			}
		}

		// Load data from averages table for display
		public List<WeatherDisplay> LoadAverages()
		{
			var qGetAverages = (
				from averages in db.averages
				join region in db.region on averages.region_id equals region.region_id
				select new WeatherDisplay
				{
					id = averages.averages_id,
					region = region.name,
					temperature = averages.temperature,
					pressure = averages.pressure,
					humidity = averages.humidity,
					wind_speed = averages.wind_speed
				}).ToList();

			return qGetAverages;
		}
	}
}