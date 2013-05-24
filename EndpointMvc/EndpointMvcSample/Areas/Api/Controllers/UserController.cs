using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvcSample.Areas.Api.Models;

namespace EndpointMvcSample.Areas.Api.Controllers {
	[Endpoint]
	[Description("User interaction endpoints v1")]
	public class UserController : Controller {

		[Description ( "Gets registered users" )]
		public ActionResult List ( ) {
			// all your code here to return your users api
			return new EmptyResult ( );
		}

		[RequiresAuthentication]
		[Description ( "Adds a new user to the system." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Put )]
		public ActionResult Add (
			[Required]
			[Description ( "The user model" )]
			UserModel user ) {
			// code to add the new user from the model
			return new EmptyResult ( );
		}

		[RequiresAuthentication]
		[Description ( "Deleted a user from the system." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Delete )]
		public ActionResult Delete (
				[Required]
				[Description ( "The user id" )]
				string id
			) {
			// delete the user
			return new EmptyResult ( );
		}

		[Ignore]
		[RequiresAuthentication]
		[Description ( "Deleted a user from the system." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Delete )]
		public ActionResult Delete ( 
			[Required]
			[Description ( "The user id" )]
			UserModel user 
		) {
			return Delete ( user.Username );
		}

	}
}
