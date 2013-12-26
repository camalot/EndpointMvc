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
			RegisterViewSearchLocations ( );

			routes.MapRoute (
				name: DEFAULT_NAME.With ( "Define" ),
				url: "define/{id}/{action}",
				defaults: new { controller = "Define" },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);

			routes.MapRoute (
				name: DEFAULT_NAME.With ( "Default" ),
				url: "endpoints/{action}/{id}",
				defaults: new { controller = DEFAULT_CONTROLLER, action = DEFAULT_ACTION, id = UrlParameter.Optional },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);

		}

		/// <summary>
		/// Registers the routes for EndpointMvc for all areas.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutesForAllAreas ( RouteCollection routes ) {
			routes.RegisterEnpointMvcForAllAreas ( );
		}

		/// <summary>
		/// Registers the routes for EndpointMvc for the specified area.
		/// </summary>
		/// <param name="context">The context.</param>
		public static void RegisterRoutesForArea ( AreaRegistrationContext context ) {
			context.Routes.MapRoute (
				name: DEFAULT_NAME.With ( context.AreaName ),
				url: DEFAULT_URL,
				defaults: new { controller = DEFAULT_CONTROLLER, action = DEFAULT_ACTION, area = context.AreaName, id = UrlParameter.Optional },
				namespaces: new String[] { DEFAULT_NAMESPACE }
			);
		}


		private static void RegisterViewSearchLocations ( ) {
			var viewEngines = ViewEngines.Engines.Where ( e => e.Is<RazorViewEngine> ( ) ).Cast<RazorViewEngine>();
			if ( viewEngines != null && viewEngines.Count() > 0  ) {
				foreach ( var viewEngine in viewEngines ) {
					viewEngine.PartialViewLocationFormats = viewEngine.PartialViewLocationFormats.Concat ( new[] {
						"~/Views/Endpoints/{0}.cshtml",
						"~/Views/Endpoints/EditorTemplates/{0}.cshtml",
						"~/Views/Endpoints/DisplayTemplates/{0}.cshtml",
						"~/Views/Endpoints/Shared/{0}.cshtml",
						"~/Views/Endpoints/{0}.vbhtml",
						"~/Views/Endpoints/EditorTemplates/{0}.vbhtml",
						"~/Views/Endpoints/DisplayTemplates/{0}.vbhtml",
						"~/Views/Endpoints/Shared/{0}.vbhtml",
					} ).ToArray ( );

					viewEngine.ViewLocationFormats = viewEngine.ViewLocationFormats.Concat ( new[] {
						"~/Views/Endpoints/{0}.cshtml",
						"~/Views/Endpoints/Shared/{0}.cshtml",
						"~/Views/Endpoints/{0}.vbhtml",
						"~/Views/Endpoints/Shared/{0}.vbhtml",
					} ).ToArray ( );
				}
			}
		}
	}
}
