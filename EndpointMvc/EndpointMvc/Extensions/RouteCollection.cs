using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvc {
		/// <summary>
		/// Registers the endpoint MVC routes.
		/// </summary>
		/// <param name="rc">The route collect.</param>
		/// <returns></returns>
		public static RouteCollection RegisterEndpointMvc ( this RouteCollection rc ) {
			rc.MapRoute (
					name: "EndpointMvc_Default",
					url: "{area}/endpoints/{action}",
					defaults: new { controller = "Endpoints", action = "Json" },
					namespaces: new String[] { "EndpointMvc.Controllers" }
			);

			/*GetRegisteredAreas ( ).ForEach ( a => {
				AreaRegistrationContext context = new AreaRegistrationContext ( a.AreaName, rc, null );
				context.RegisterEndpointMvcForArea ( a.AreaName );
			} );*/
			return rc;
		}

		/// <summary>
		/// Registers the endpoint MVC routes for area.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="area">The area.</param>
		/// <returns></returns>
		public static AreaRegistrationContext RegisterEndpointMvcForArea ( this AreaRegistrationContext context, String area ) {
			context.MapRoute (
					name: "{0}_EndpointMvc_Default".With ( area.Require ( ) ),
					url: "{area}/endpoints/{action}",
					defaults: new { controller = "Endpoints", action = "Json", area = area },
					namespaces: new String[] { "EndpointMvc.Controllers" }
			);
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
