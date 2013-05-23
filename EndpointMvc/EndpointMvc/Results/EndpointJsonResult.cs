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
	class EndpointJsonResult<T> : EndpointResult<T> {
		public EndpointJsonResult ( T data ) : base(data){
		}

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
