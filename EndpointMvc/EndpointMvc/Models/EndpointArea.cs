using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Models {
	[XmlRoot("Area")]
	public class EndpointArea {
		public EndpointArea ( ) {
			Services = new Dictionary<string, EndpointService> ( );
		}
		[XmlAttribute]
		public String Name { get; set; }

		[XmlIgnore]
		public Dictionary<String, EndpointService> Services { get; set; }

		[XmlElement("Service")]
		[JsonIgnore]
		public List<EndpointService> SerivcesList {
			get {
				return this.Services.Select ( v => v.Value ).ToList();
			}
		}

	}
}
