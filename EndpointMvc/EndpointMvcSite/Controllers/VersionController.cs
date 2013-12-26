using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndpointMvcSite.Controllers {
	public class VersionController : Controller {
		// GET: /Version/
		public ActionResult Install ( string id, string v ) {
			// todo: Tracking of install
			return RedirectToAction ( "", new { controller = "Home" } );
		}
	}
}