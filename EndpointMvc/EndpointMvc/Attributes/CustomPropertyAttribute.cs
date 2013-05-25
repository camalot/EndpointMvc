using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Extensions;

namespace EndpointMvc.Attributes {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
	public class CustomPropertyAttribute : Attribute{
		public CustomPropertyAttribute ( String name ) {
			Name = name.Require();
			
		}

		public String Name { get; set; }
		public Object Value { get; set; }
		public String Description { get; set; }
	}
}
