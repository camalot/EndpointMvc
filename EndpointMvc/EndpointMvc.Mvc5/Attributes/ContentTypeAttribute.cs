using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Extensions;
using Camalot.Common.Extensions;

namespace EndpointMvc.Attributes {
	/// <summary>
	/// Specifies the content type
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class ContentTypeAttribute : Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTypeAttribute"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public ContentTypeAttribute ( String type ) {
			ContentType = type.Require ( );
		}

		/// <summary>
		/// Gets or sets the type of the content.
		/// </summary>
		/// <value>
		/// The type of the content.
		/// </value>
		public String ContentType { get; set; }

	}
}
