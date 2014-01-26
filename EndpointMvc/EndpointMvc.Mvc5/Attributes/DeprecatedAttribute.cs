using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Extensions;
using Camalot.Common.Extensions;

namespace EndpointMvc.Attributes {
	/// <summary>
	/// Describes an endpoint as deprecated
	/// </summary>
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method )]
	public class DeprecatedAttribute : Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="DeprecatedAttribute"/> class.
		/// </summary>
		public DeprecatedAttribute ( ) : this("This endpoint is deprecated"){

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DeprecatedAttribute"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public DeprecatedAttribute ( String message ) {
			Message = message.Require ( );
		}
		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public String Message { get; set; }
	}
}
