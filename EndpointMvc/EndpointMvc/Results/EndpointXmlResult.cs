using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Serialization;

namespace EndpointMvc.Results {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	sealed class EndpointXmlResult<T> : EndpointResult<T> {
		public EndpointXmlResult ( T data )
			: base ( data ) {
		}

		/// <summary>
		/// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
		/// </summary>
		/// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
		public override void ExecuteResult ( ControllerContext context ) {
			context.HttpContext.Response.ContentType = "text/xml";
			var ser = XmlSerializerFactory.Create<T> ( );
			ser.Serialize ( context.HttpContext.Response.OutputStream, this.Data );
		}
	}
}
