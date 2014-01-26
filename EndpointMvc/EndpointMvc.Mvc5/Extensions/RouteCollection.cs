using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using MoreLinq;
using Camalot.Common.Extensions;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {
		/// <summary>
		/// Registers the EnpointMVC routes.
		/// </summary>
		/// <param name="rc">The route collect.</param>
		/// <returns></returns>
		public static RouteCollection RegisterEndpointMvc ( this RouteCollection rc ) {
			EndpointMvc.Config.RouteConfig.RegisterRoutes ( rc );
			return rc;
		}

		/// <summary>
		/// Registers the EnpointMVC routes for all areas.
		/// </summary>
		/// <param name="rc">The rc.</param>
		/// <returns></returns>
		public static RouteCollection RegisterEnpointMvcForAllAreas ( this RouteCollection rc ) {
			GetRegisteredAreas ( ).ForEach ( a => {
				AreaRegistrationContext context = new AreaRegistrationContext ( a.AreaName, rc, null );
				context.RegisterEndpointMvcForArea ( );
			} );
			return rc;
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
			foreach ( var area in AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies ( ).SelectMany ( asm => asm.GetTypesThatAre<AreaRegistration> ( ) ) ) {
				var instance = (AreaRegistration)Activator.CreateInstance ( area );
				areas.Add ( instance );
			}
			return areas;
		}
	}
}
