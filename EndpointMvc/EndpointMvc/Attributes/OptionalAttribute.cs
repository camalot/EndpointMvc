using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	[AttributeUsage(AttributeTargets.Parameter)]
	public class OptionalAttribute : Attribute{
	}
}
