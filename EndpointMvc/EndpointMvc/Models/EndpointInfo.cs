using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndpointMvc.Models {
	public class EndpointInfo {
		[XmlAttribute]
		public String Name { get; set; }
		[XmlElement]
		public String Description { get; set; }
		[XmlElement]
		public String Url { get; set; }
		[XmlArray("Methods")]
		[XmlArrayItem("Method")]
		public List<String> HttpMethods { get; set; }
		[XmlArray("Params")]
		[XmlArrayItem("Param")]
		public List<ParamInfo> Params { get; set; }
		[XmlAttribute]
		public bool Obsolete { get; set; }
		[XmlAttribute]
		public bool Deprecated { get; set; }
		[XmlAttribute]
		public String SinceVersion { get; set; }
		[XmlAttribute]
		public bool RequiresAuthentication { get; set; }
	}
}
