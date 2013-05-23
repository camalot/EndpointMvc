using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Results;

namespace EndpointMvc.Extensions {
	/// <summary>
	/// 
	/// </summary>
	public static partial class EndpointMvc {
		/// <summary>
		/// Exports to json.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static ActionResult EndpointJson<T> ( this Controller controller, T data ) {
			return new EndpointJsonResult<T> ( data );
		}

		/// <summary>
		/// Exports to json.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static ActionResult EndpointJson ( this Controller controller, Object data ) {
			return EndpointJson<Object> ( controller, data );
		}

		/// <summary>
		/// Exports to xml.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static ActionResult EndpointXml<T> ( this Controller controller, T data ) {
			return new EndpointXmlResult<T> ( data );
		}

		/// <summary>
		/// Exports to xml.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static ActionResult EndpointXml ( this Controller controller, Object data ) {
			return EndpointXml<Object> ( controller, data );
		}
	}
}
