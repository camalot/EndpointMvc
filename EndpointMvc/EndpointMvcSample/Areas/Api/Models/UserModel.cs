using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndpointMvcSample.Areas.Api.Models {
	public class UserModel {
		[Required]
		[MaxLength ( 100 )]
		[Description ( "The user name" )]
		public String Username { get; set; }
		[DataType ( DataType.EmailAddress )]
		[Required]
		[Description ( "The users email address" )]
		public String Email { get; set; }
		[Description ( "The users display name" )]
		public String DisplayName { get; set; }
	}
}