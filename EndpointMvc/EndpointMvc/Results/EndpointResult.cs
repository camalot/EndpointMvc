using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EndpointMvc.Results {
	abstract class EndpointResult<T> : ActionResult {
		public EndpointResult ( T data ) {
			this.Data = data;
		}
		public T Data { get; private set; }
	}
}
