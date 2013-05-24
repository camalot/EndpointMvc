﻿using System.Web.Mvc;
using EndpointMvc.Extensions;
namespace EndpointMvcSample.Areas.Api {
	public class ApiAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Api";
			}
		}

		public override void RegisterArea ( AreaRegistrationContext context ) {
			context.RegisterEndpointMvcForArea ( AreaName );
			context.MapRoute (
					"Api_default",
					"Api/{controller}/{action}/{id}",
					new { action = "List", id = UrlParameter.Optional }
			);
		}
	}
}
