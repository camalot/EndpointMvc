using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvcSample.Areas.Api.Models;

namespace EndpointMvcSample.Areas.V2.Controllers {
	[Endpoint]
	[Description ( "User interaction endpoints v2" )]
	public class UserController : EndpointMvcSample.Areas.Api.Controllers.UserController {

		[SinceVersion("2.0")]
		[RequiresAuthentication]
		[Description ( "Locks a user account." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Put )]
		public bool Lock ( String id ) {
			// lock the user
			return false;
		}

	}
}
