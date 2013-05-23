using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Results;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvc {
		public static ActionResult EndpointJson<T> ( this Controller controller, T data ) {
			return new EndpointJsonResult<T> ( data );
		}

		public static ActionResult EndpointJson ( this Controller controller, Object data ) {
			return EndpointJson<Object> ( controller, data );
		}

		public static ActionResult EndpointXml<T> ( this Controller controller, T data ) {
			return new EndpointXmlResult<T> ( data );
		}

		public static ActionResult EndpointXml ( this Controller controller, Object data ) {
			return EndpointXml<Object> ( controller, data );
		}
	}
}
