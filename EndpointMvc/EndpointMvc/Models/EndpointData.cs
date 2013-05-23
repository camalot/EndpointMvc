using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EndpointMvc.Extensions;

namespace EndpointMvc.Models {
	[XmlRoot ( "Areas" )]
	public class EndpointData {

		public EndpointData ( ) {
		}

		[XmlElement("Area")]
		public List<EndpointArea> Areas { get; set; }
	}
}
