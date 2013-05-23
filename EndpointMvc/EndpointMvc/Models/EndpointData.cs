using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EndpointMvc.Extensions;

namespace EndpointMvc.Models {
	/// <summary>
	/// 
	/// </summary>
	[XmlRoot ( "Areas" )]
	public class EndpointData {

		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointData"/> class.
		/// </summary>
		public EndpointData ( ) {
		}

		/// <summary>
		/// Gets or sets the areas.
		/// </summary>
		/// <value>
		/// The areas.
		/// </value>
		[XmlElement("Area")]
		public List<EndpointArea> Areas { get; set; }
	}
}
