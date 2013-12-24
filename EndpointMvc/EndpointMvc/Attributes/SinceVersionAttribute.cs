using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Attributes {
	/// <summary>
	/// Indicates what version an endpoint was made available
	/// </summary>
	[AttributeUsage ( AttributeTargets.Class | AttributeTargets.Method )]
	public class SinceVersionAttribute : Attribute{
		/// <summary>
		/// Initializes a new instance of the <see cref="SinceVersionAttribute"/> class.
		/// </summary>
		/// <param name="version">The version.</param>
		public SinceVersionAttribute ( String version ) {
			Version = new Version ( version );
		}
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>

		public Version Version { get; set; }

	}
}
