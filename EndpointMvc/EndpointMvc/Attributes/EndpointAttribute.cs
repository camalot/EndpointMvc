using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace EndpointMvc.Attributes {
	/// <summary>
	/// Describes a class as an EnpointMvc endpoint
	/// </summary>
	[AttributeUsage ( AttributeTargets.Class )]
	public class EndpointAttribute : Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointAttribute" /> class.
		/// </summary>
		public EndpointAttribute ( ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public EndpointAttribute ( String name ) {
			Name = name;
		}

		/// <summary>
		/// Gets or sets the name of the endpoint.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public String Name { get; set; }
	}
}
