using System.Web.Mvc;
using WeatherAggregator.Repositories;

namespace WeatherAggregator.Controllers
{
	public class WeatherController : Controller
	{
		// Display data from averages table
		public ActionResult Index()
		{
			WeatherRepo repo = new WeatherRepo();
			return View(repo.LoadAverages());
		}

		// Update data for the selected region 
		public ActionResult UpdateData(int region_id)
		{
			WeatherRepo repo = new WeatherRepo();
			return Json(repo.LoadRawData(region_id), JsonRequestBehavior.AllowGet);
		}
	}
}