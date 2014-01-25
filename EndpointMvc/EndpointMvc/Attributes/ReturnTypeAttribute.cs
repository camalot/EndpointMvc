using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Extensions;
using Camalot.Common.Extensions;

namespace EndpointMvc.Attributes {
	[AttributeUsage( AttributeTargets.Method, AllowMultiple = false )]
	public class ReturnTypeAttribute : Attribute {
		public ReturnTypeAttribute ( Type returnType ) {
			ReturnType = returnType.Require ( );
		}

		public Type ReturnType { get; set; }
	}
}
