using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter )]
	public class IgnoreAttribute : Attribute {
		public IgnoreAttribute ( ) {

		}
	}
}
