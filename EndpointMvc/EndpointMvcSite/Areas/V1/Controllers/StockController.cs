using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvcSite.Areas.V1.Models;

namespace EndpointMvcSite.Areas.V1.Controllers {
	[Endpoint]
	[Description("Service to get stock symbol information.")]
	[RequiresAuthentication]
	public class StockController : Controller {
		[Description("Get a list of the symbols")]
		[ReturnType ( typeof ( StockSymbolModel[] ) )]
		[AcceptVerbs ( HttpVerbs.Get )]
		public ActionResult List ( ) {
      return new EmptyResult();
    }


	}
}