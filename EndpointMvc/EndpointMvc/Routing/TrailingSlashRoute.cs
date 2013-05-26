using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace EndpointMvc.Routing {
	public class TrailingSlashRoute : Route {
		public TrailingSlashRoute ( string url, IRouteHandler routeHandler )
			: base ( url, routeHandler ) { }

		public TrailingSlashRoute ( string url, RouteValueDictionary defaults, IRouteHandler routeHandler )
			: base ( url, defaults, routeHandler ) { }

		public TrailingSlashRoute ( string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
													IRouteHandler routeHandler )
			: base ( url, defaults, constraints, routeHandler ) { }

		public TrailingSlashRoute ( string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
													RouteValueDictionary dataTokens, IRouteHandler routeHandler )
			: base ( url, defaults, constraints, dataTokens, routeHandler ) { }

		public override VirtualPathData GetVirtualPath ( RequestContext requestContext, RouteValueDictionary values ) {
			VirtualPathData path = base.GetVirtualPath ( requestContext, values );

			if ( path != null )
				path.VirtualPath = path.VirtualPath + "/";
			return path;
		}
	}
}
