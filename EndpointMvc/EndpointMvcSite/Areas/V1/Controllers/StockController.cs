using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;

namespace EndpointMvcSite.Areas.V1.Controllers {
	[Endpoint]
	[Description("Service to get stock symbol information.")]
	public class StockController : Controller {
		public ActionResult List ( ) {
      return new EmptyResult();
    }


	}
}