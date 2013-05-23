using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method )]
	public class DeprecatedAttribute : Attribute {
		public DeprecatedAttribute ( ) {

		}
		public String Message { get; set; }
	}
}
