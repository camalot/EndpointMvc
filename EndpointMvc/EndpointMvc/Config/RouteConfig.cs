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
		private const String DEFAULT_ACTION = "Html";
		private const String DEFAULT_CONTROLLER = "Endpoints";
		private const String DEFAULT_URL = "{area}/endpoints/{action}/{id}";
		private const String DEFAULT_NAMESPACE = "EndpointMvc.Controllers";
		private const String DEFAULT_NAME = "{0}_EndpointMvc";
		/// <summary>
		/// Registers the routes for EndpointMvc.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutes ( RouteCollection routes ) {
			/*routes.MapRouteWithTrailingSlash (
				name: DEFAULT_NAME.With ( "Define" ),
				url: "define/{action}/{*id}",
				defaults: new { controller = "Endpoints", action = DEFAULT_ACTION },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);*/

			routes.MapRoute (
				name: DEFAULT_NAME.With("Default"),
				url: "endpoints/{action}/{id}",
				defaults: new { controller = DEFAULT_CONTROLLER, action = DEFAULT_ACTION, id = UrlParameter.Optional },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);
		}

		public static void RegisterRoutesForAllAreas ( RouteCollection routes ) {
			routes.RegisterEnpointMvcForAllAreas ( );
		}

		public static void RegisterRoutesForArea ( AreaRegistrationContext context ) {
			/*context.Routes.MapRouteWithTrailingSlash (
				name: DEFAULT_NAME.With ( "{0}_Define".With( context.AreaName ) ),
				url: DEFAULT_URL,
				defaults: new { controller = "Endpoints", action = DEFAULT_ACTION, area = context.AreaName, id = UrlParameter.Optional },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);*/

			context.Routes.MapRoute (
				name: DEFAULT_NAME.With ( context.AreaName ),
				url: DEFAULT_URL,
				defaults: new { controller = DEFAULT_CONTROLLER, action = DEFAULT_ACTION, area = context.AreaName, id = UrlParameter.Optional },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);
		}
	}
}
