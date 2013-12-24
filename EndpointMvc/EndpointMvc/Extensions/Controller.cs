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
	public static partial class EndpointMvcExtensions {

		public static EndpointResult EndpointJson<T> ( this Controller controller, T data ) {
			return new EndpointJsonResult<T> ( data );
		}

		/// <summary>
		/// Exports to xml.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static EndpointResult EndpointXml<T> ( this Controller controller, T data ) {
			return new EndpointXmlResult<T> ( data );
		}
	}
}
