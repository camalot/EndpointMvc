using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;

namespace EndpointMvc.Site.Areas.V2.Controllers
{
	[Endpoint]
	[Description ( "User interaction endpoints v2" )]
	public class UserController : EndpointMvc.Site.Areas.V1.Controllers.UserController {

		[SinceVersion ( "2.0" )]
		[RequiresAuthentication]
		[Description ( "Locks a user account." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Put )]
		public bool Lock ( String id ) {
			// lock the user
			return false;
		}

		[SinceVersion ( "2.0" )]
		[RequiresAuthentication]
		[Description ( "Unlocks a user account." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Put )]
		public bool Unlock ( String id ) {
			return false;
		}

		[NonAction]
		public bool NotAnAction ( ) {
			return true;
		}

	}
}