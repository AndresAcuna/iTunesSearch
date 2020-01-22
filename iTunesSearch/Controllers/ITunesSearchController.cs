using iTunesSearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace iTunesSearch.Controllers
{
    public class ITunesSearchController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Index(String searchTextBox)
        {
            ViewBag.Title = "Search results for " + searchTextBox;

            using (var client = new HttpClient())
            {
                var stringTask = client.GetStringAsync(String.Format("https://itunes.apple.com/search?term={0}", searchTextBox));
                var msg = await stringTask;

                ITunesResponse response = JsonConvert.DeserializeObject<ITunesResponse>(msg);

                return View("Index", response);
                
            }

        }
        public ActionResult Index()
        {

            ViewBag.Title = "Search iTunes";
            return View();
        }
    }
}