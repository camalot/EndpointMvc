using System;
using System.Collections.Generic;
using System.Linq;
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
					url: "{area}/endpoints/{action}/{id}",
					defaults: new { controller = "Endpoints", action = "Json", id = UrlParameter.Optional },
					namespaces: new String[] { "EndpointMvc.Controllers" }
			);
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
					name: "{0}_EndpointMvc_Default".With(area.Require()),
					url: "{area}/endpoints/{action}/{id}",
					defaults: new { controller = "Endpoints", action = "Json", area = area, id = UrlParameter.Optional },
					namespaces: new String[] { "EndpointMvc.Controllers" }
			);
			return context;
		}
	}
}
