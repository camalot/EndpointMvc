using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvc.Site.Areas.V1.Models;

namespace EndpointMvc.Site.Areas.V1.Controllers
{
	[Endpoint]
	[Description ( "User interaction endpoints v1" )]
	[CustomProperty ( "Daily Limit", Value = 1000, Description = "Maximum number of requests in a 24 hour period." )]
	[CustomProperty ( "Hourly Limit", Value = 100, Description = "Maximum number of requests in a 60 minute period." )]
	[CustomProperty ( "Awesome", Value = true )]
	[Gist ( "d4d2110ca4769876fb69", Title = "Gist Title", Description = "My Example Gist" )]
	public class UserController : Controller {
		[Description ( "Gets registered users" )]
		[CustomProperty ( "Daily Limit", Value = 0 )]
		[Gist ( "d4d2110ca4769876fb69" )]
		[ReturnType ( typeof ( IEnumerable<UserModel> ) )]
		public ActionResult List ( ) {
			// all your code here to return your users api
			return null;
		}

		[RequiresAuthentication]
		[Description ( "Adds a new user to the system." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Put )]
		[Gist ( "d4d2110ca4769876fb69", Title = "Gist With a Title" )]
		[ReturnType ( typeof ( UserModel ) )]
		public ActionResult Add (
			[Required]
			[Description ( "The user model" )]
			UserModel user ) {
			// code to add the new user from the model
			return new EmptyResult ( );
		}

		[RequiresAuthentication]
		[Description ( "Deleted a user from the system." )]
		[AcceptVerbs ( HttpVerbs.Get )]
		[ReturnType ( typeof ( UserModel ) )]
		public ActionResult Delete (
				[Required]
				[Description ( "The user id" )]
				[CustomProperty ( "Max Length", Value = 100 )]
				string id
			) {
			// delete the user
			return new EmptyResult ( );
		}

		[RequiresAuthentication]
		[Description ( "Deleted a user from the system." )]
		[AcceptVerbs ( HttpVerbs.Post | HttpVerbs.Delete )]
		[ReturnType ( typeof ( UserModel ) )]
		public ActionResult Delete (
			[Required]
			[Description ( "The user id" )]
			UserModel user
		) {
			return Delete ( user.Username );
		}

		public string GetUser ( String id ) {
			return String.Empty;
		}

		[Description ( "Find a user id by email" )]
		[Deprecated ( "This method was replaced by Get." )]
		public ActionResult FindUser ( String id ) {
			return new EmptyResult ( );
		}

		[Description ( "Find a user id by email" )]
		[Obsolete ( "This method is no longer supported." )]
		public ActionResult FindUserId ( String email ) {

			return new EmptyResult ( );
		}
	}
}