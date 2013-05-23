using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Models {
	[XmlRoot ( "Service" )]
	public class EndpointService {
		public EndpointService ( ) {
			Endpoints = new Dictionary<String, EndpointInfo> ( );
		}

		[XmlAttribute]
		public String Name { get; set; }
		[XmlElement ( "Description" )]
		public String Description { get; set; }
		[XmlIgnore]
		public Dictionary<String, EndpointInfo> Endpoints { get; set; }
		[XmlElement ( "Endpoint" )]
		[JsonIgnore]
		public List<EndpointInfo> EndpointList {
			get {
				return Endpoints.Select ( e => e.Value ).ToList ( );
			}
		}
	}
}
