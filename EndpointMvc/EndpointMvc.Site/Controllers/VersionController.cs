using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndpointMvc.Site.Controllers {
	public class VersionController : Controller {
		// GET: /Version/Install/Package/?v=VERSION
		public ActionResult Install ( string id, string v ) {
			return RedirectToAction ( "", new { controller = "Home" } );
		}
		// GET: /Version/Uninstall/Package/?v=VERSION
		public ActionResult Uninstall ( string id, string v ) {
			return RedirectToAction ( "", new { controller = "Home" } );
		}
	}
}