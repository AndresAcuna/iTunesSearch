using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTunesSearch.Models;

namespace iTunesSearch.Controllers
{
    public class AnalyticResultsController : Controller
    {
        private iTunesSearchEntities db = new iTunesSearchEntities();

        // GET: AnalyticResults
        public ActionResult Index()
        {

            var hits = db.hits;
            var analytics = db.analytics;

            var detailedHits = hits.Join(analytics, a => a.trackId, h => h.trackId, (a, h) => new { a, h })
                .Select(dh => new
                {
                    hitCount = dh.h.hitCount,
                    title = dh.a.title,
                    artist = dh.a.artist,
                    trackURL = dh.a.url,
                    trackId = dh.h.trackId
                }).OrderByDescending(hC => hC.hitCount);


            var jsonDetailedHits = new { iTunesHits = detailedHits };

            return Json(jsonDetailedHits, JsonRequestBehavior.AllowGet);
            //return View(analytics);
        }

        // GET: AnalyticResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            analytic analytic = db.analytics.Find(id);
            if (analytic == null)
            {
                return HttpNotFound();
            }
            return View(analytic);
        }

    }
}
