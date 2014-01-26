using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndpointMvc.Site.Areas.V1.Controllers
{
    public class StockController : Controller
    {
        //
        // GET: /V1/Stock/
        public ActionResult Index()
        {
            return View();
        }
	}
}