using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EndpointMvc.Routing;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {
		/// <summary>
		/// Registers the endpoint MVC routes.
		/// </summary>
		/// <param name="rc">The route collect.</param>
		/// <returns></returns>
		public static RouteCollection RegisterEndpointMvc ( this RouteCollection rc ) {
			EndpointMvc.Config.RouteConfig.RegisterRoutes ( rc );
			return rc;
		}

		public static RouteCollection RegisterEnpointMvcForAllAreas ( this RouteCollection rc ) {
			GetRegisteredAreas ( ).ForEach ( a => {
				AreaRegistrationContext context = new AreaRegistrationContext ( a.AreaName, rc, null );
				context.RegisterEndpointMvcForArea ( );
			} );
			return rc;
		}

		public static Route MapRouteWithTrailingSlash ( this RouteCollection routes, string name, string url, object defaults ) {
			return routes.MapRouteWithTrailingSlash ( name, url, defaults, null );
		}

		public static Route MapRouteWithTrailingSlash ( this RouteCollection routes, string name, string url, object defaults, object constraints ) {
			return routes.MapRouteWithTrailingSlash(name,url,defaults,constraints, null);
		}

		public static Route MapRouteWithTrailingSlash ( this RouteCollection routes, string name, string url, object defaults, String[] namespaces ) {
			return routes.MapRouteWithTrailingSlash ( name, url, defaults, new { }, namespaces );
		}


		public static Route MapRouteWithTrailingSlash ( this RouteCollection routes, string name, string url, object defaults, object constraints, String[] namespaces ) {
			routes.Require ( );
			var route = new TrailingSlashRoute ( url.Require(), new MvcRouteHandler ( ) ) {
				Defaults = new RouteValueDictionary ( defaults ),
				Constraints = new RouteValueDictionary ( constraints ),
			};

			if ( ( namespaces != null ) && ( namespaces.Length > 0 ) ) {
				route.DataTokens = new RouteValueDictionary ( );
				route.DataTokens["Namespaces"] = namespaces;
			}

			if ( String.IsNullOrEmpty ( name ) )
				routes.Add ( route );
			else
				routes.Add ( name, route );
			return route;
		}

		/// <summary>
		/// Registers the endpoint MVC routes for area.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="area">The area.</param>
		/// <returns></returns>
		public static AreaRegistrationContext RegisterEndpointMvcForArea ( this AreaRegistrationContext context ) {
			EndpointMvc.Config.RouteConfig.RegisterRoutesForArea ( context );
			return context;
		}

		private static IEnumerable<AreaRegistration> GetRegisteredAreas ( ) {
			var areas = new List<AreaRegistration> ( );
			foreach ( var area in AppDomain.CurrentDomain.GetAssemblies ( ).SelectMany ( asm => asm.GetTypesThatAre<AreaRegistration> ( ) ) ) {
				var instance = (AreaRegistration)Activator.CreateInstance ( area );
				areas.Add ( instance );
			}
			return areas;
		}
	}
}
