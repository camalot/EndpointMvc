﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvcSite.Areas.V1.Models;
using EndpointMvcSite.Models;

namespace EndpointMvcSite.Areas.V1.Controllers {
	[Endpoint]
	[Description("Service to get specific market information.")]
	public class MarketController : Controller{
		[ReturnType(typeof(IEnumerable<MarketModel>))]
		[AcceptVerbs ( HttpVerbs.Get )]
		[RequiresAuthentication]
		public ActionResult List ( ) {
			return new EmptyResult ( );
		}

		[AcceptVerbs ( HttpVerbs.Get )]
		[RequiresAuthentication]
		public async Task<MarketModel> Get ( MarketTypes id ) {
			return await Task.FromResult<MarketModel>( new MarketModel () );
		}
	}
}