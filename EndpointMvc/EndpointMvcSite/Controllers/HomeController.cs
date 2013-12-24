using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndpointMvcSite.Controllers {
	public class HomeController : Controller {
		public ActionResult Index ( ) {
			return View ( );
		}

		[ChildActionOnly]
		public PartialViewResult Gist ( String id ) {
			return PartialView ( model: id );
		}
	}
}