using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method )]
	public class SinceVersionAttribute : Attribute{
		public SinceVersionAttribute ( String version ) {
			Version = new Version ( version );
		}
		public Version Version { get; set; }

	}
}
