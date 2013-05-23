using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EndpointMvc.Results {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	abstract class EndpointResult<T> : ActionResult {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointResult{T}"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public EndpointResult ( T data ) {
			this.Data = data;
		}
		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public T Data { get; private set; }
	}
}
