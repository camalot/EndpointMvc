using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage ( AttributeTargets.Class )]
	public class EndpointAttribute : Attribute {
		public EndpointAttribute ( ) {

		}

		public EndpointAttribute ( String name ) {
			Name = name;
		}

		public String Name { get; set; }
	}
}
