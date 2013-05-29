using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Extensions;

namespace EndpointMvc.Attributes {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true )]
	public class GistAttribute : Attribute {
		public GistAttribute ( String gistId ) {
			GistId = gistId.Require ( );
		}

		public String Title { get; set; }
		public String Description { get; set; }
		public String GistId { get; set; }
	}
}
