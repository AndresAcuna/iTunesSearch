using iTunesSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTunesSearch.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult Index()
        {
            ViewBag.Title = "iTunes search analytics";

            List<analytic> analyticsResults = null;

            using(var db = new iTunesSearchEntities())
            {
                //analyticsResults = db.analytics.OrderByDescending(a => a.hitCount).ToList();
                analyticsResults = db.analytics.Include("hit").OrderByDescending(a => a.hitCount).ToList();
            }

            return View(analyticsResults);
        }
        
        public ActionResult IncrementRedirect(Result result)
        {
            int count = 0;
            bool exists = false;
            analytic dbAnalytic = null;
            hit dbhit = null;

            //Find out if it exists
            using (var db = new iTunesSearchEntities())
            {

                count = db.analytics.Count();

                //if the value exists, update
                if (db.hits.Any(a => a.url == result.ViewURL))
                {
                    exists = true;
                    dbAnalytic = db.analytics.Where(a => a.hit.url == result.ViewURL).FirstOrDefault<analytic>();
                }else //otherwise add new hit to database
                {
                    exists = false;
                    
                    dbhit = new hit
                    {
                        trackId = ++count,
                        url = result.ViewURL,
                        title = result.TrackName,
                        artist = result.ArtistName,
                        artworkurl = result.ArtworkURL100,

                    };

                    db.hits.Add(dbhit);
                    db.SaveChanges();

                    
                }
            }

            //change without DBContext or create
            if (exists && dbAnalytic != null)
            {
                dbAnalytic.hitCount++;
            }
            else
            {

                dbAnalytic = new analytic
                {
                    trackId = count,
                    hitCount = 1,
                    //hit = dbhit,
                };
            }

            using (var db = new iTunesSearchEntities())
            {
                if (exists)
                {
                    db.Entry(dbAnalytic).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.analytics.Add(dbAnalytic);
                }

                db.SaveChanges();
            }

            return Redirect(result.ViewURL);
        }
    }
}
