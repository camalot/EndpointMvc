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
		/// <summary>
		/// Registers the routes for EndpointMvc.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutes ( RouteCollection routes ) {
			routes.RegisterEndpointMvc ( );
		}

		/// <summary>
		/// Registers the routes for EndpointMvc to a specific area.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="area">The area.</param>
		public static void RegisterRoutes ( AreaRegistrationContext context, String area ) {
			context.RegisterEndpointMvcForArea ( area );
		}

	}
}
