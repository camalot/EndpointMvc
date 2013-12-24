using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Models;
using EndpointMvc.Reflection;
using EndpointMvc.Extensions;

namespace EndpointMvc.Controllers {
	public class DefineController : Controller {
		public Results.EndpointResult Json ( String id ) {
			throw new NotImplementedException ( );
		}

		public Results.EndpointResult Xml ( String id ) {
			throw new NotImplementedException ( );
		}

		public ActionResult Html ( String id ) {
			var reflector = new Reflector();
			var type = reflector.GetTypeFromName ( id );
			var model = new DefineData {
				Name = type.Name,
				QualifiedName = type.QualifiedName()
			};

			// the view lives in endpoints
			return View ("../Endpoints/Define", model );
		}
	}
}
