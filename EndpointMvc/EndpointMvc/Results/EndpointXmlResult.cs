using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Serialization;

namespace EndpointMvc.Results {
	class EndpointXmlResult<T> : EndpointResult<T> {
		public EndpointXmlResult ( T data )
			: base ( data ) {
		}

		public override void ExecuteResult ( ControllerContext context ) {
			context.HttpContext.Response.ContentType = "application/json";
			var ser = XmlSerializerFactory.Create<T> ( );
			ser.Serialize ( context.HttpContext.Response.OutputStream, this.Data );
		}
	}
}
