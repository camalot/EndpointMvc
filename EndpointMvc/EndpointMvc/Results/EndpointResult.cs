using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EndpointMvc.Results {
	public abstract class EndpointResult : ActionResult { }

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class EndpointResult<T> : EndpointResult {
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
