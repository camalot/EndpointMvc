using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Results {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	sealed class EndpointJsonResult<T> : EndpointResult<T> {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointJsonResult{T}"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public EndpointJsonResult ( T data ) : base(data){
		}

		/// <summary>
		/// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
		/// </summary>
		/// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
		public override void ExecuteResult ( ControllerContext context ) {
			context.HttpContext.Response.ContentType = "application/json";
			using ( var sw = new StreamWriter ( context.HttpContext.Response.OutputStream ) ) {
				using ( var jw = new JsonTextWriter ( sw ) ) {
					jw.Formatting = Formatting.None;
					jw.Indentation = 0;
					JsonSerializerFactory.Create().Serialize ( jw, this.Data );
				}
			}
		}
	}
}
