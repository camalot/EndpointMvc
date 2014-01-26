using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method )]
	public class RequiresAuthenticationAttribute : Attribute {
		public RequiresAuthenticationAttribute ( ) {

		}
	}
}
