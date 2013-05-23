using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	/// <summary>
	/// Flags an endpoint or parameter to be ignored
	/// </summary>
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter )]
	public class IgnoreAttribute : Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="IgnoreAttribute"/> class.
		/// </summary>
		public IgnoreAttribute ( ) {

		}
	}
}
