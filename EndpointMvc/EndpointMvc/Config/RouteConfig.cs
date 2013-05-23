using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EndpointMvc.Extensions;

namespace EndpointMvc.Config {
	public class RouteConfig {
		public static void RegisterRoutes ( RouteCollection routes ) {
			routes.RegisterEndpointMvc ( );
		}

		public static void RegisterRoutes ( AreaRegistrationContext context, String area ) {
			context.RegisterEndpointMvcForArea ( area );
		}

	}
}
