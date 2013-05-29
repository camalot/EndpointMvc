using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Models {
	/// <summary>
	/// An Endpoint Service
	/// </summary>
	[XmlRoot ( "Service" )]
	public class EndpointService {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndpointService"/> class.
		/// </summary>
		public EndpointService ( ) {
			Endpoints = new Dictionary<String, EndpointInfo> ( );
			Properties = new List<PropertyKeyValuePair<string, object>> ( );
			Gists = new List<Gist> ( );
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Name", Prompt = "Name" )]
		public String Name { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[XmlElement ( "Description" )]
		[Display ( Name = "Description", Prompt = "Description" )]
		public String Description { get; set; }
		/// <summary>
		/// Gets or sets the endpoints.
		/// </summary>
		/// <value>
		/// The endpoints.
		/// </value>
		[XmlIgnore]
		public Dictionary<String, EndpointInfo> Endpoints { get; set; }
		/// <summary>
		/// Gets the endpoint as a list.
		/// </summary>
		/// <value>
		/// The endpoint list.
		/// </value>
		[XmlElement ( "Endpoint" )]
		[JsonIgnore]
		public List<EndpointInfo> EndpointList {
			get {
				return Endpoints.Select ( e => e.Value ).OrderBy ( a => a.Name ).ToList ( );
			}
		}

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		/// <value>
		/// The properties.
		/// </value>
		[XmlArray ( "Properties" )]
		[XmlArrayItem ( "Property" )]
		public List<PropertyKeyValuePair<String, Object>> Properties { get; set; }

		[XmlArray("Examples")]
		[XmlArrayItem("Gist")]
		public List<Gist> Gists { get; set; }

		/// <summary>
		/// Gets or sets the qualified name.
		/// </summary>
		/// <value>
		/// the qualified name.
		/// </value>
		[XmlIgnore]
		[JsonIgnore]
		public String QualifiedName { get; set; }
	}
}
