using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Models {
	/// <summary>
	/// The endpoint area
	/// </summary>
	[XmlRoot("Area")]
	public class EndpointArea {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointArea"/> class.
		/// </summary>
		public EndpointArea ( ) {
			Services = new Dictionary<string, EndpointService> ( );
		}
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[XmlAttribute]
		public String Name { get; set; }

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		[XmlIgnore]
		public Dictionary<String, EndpointService> Services { get; set; }

		/// <summary>
		/// Gets the services as a list.
		/// </summary>
		/// <value>
		/// The services list.
		/// </value>
		[XmlElement("Service")]
		[JsonIgnore]
		public List<EndpointService> ServicesList {
			get {
				return this.Services.Select ( v => v.Value ).ToList();
			}
		}

	}
}
