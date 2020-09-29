using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    public class CronController : Controller
    {
        // GET: Cron
        public ActionResult SendEmails()
        {
            PubSubRepository psr = new PubSubRepository();
            psr.DownloadEmailFromQueueAndSend();

            return Content("Sent");
        }
    }
}