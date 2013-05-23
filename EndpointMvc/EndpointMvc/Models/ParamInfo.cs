using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndpointMvc.Models {
	public class ParamInfo {
		[XmlAttribute]
		public String Name { get; set; }
		[XmlAttribute]
		public String Type { get; set; }
		[XmlAttribute]
		public bool Optional { get; set; }
		[XmlElement]
		public String Description { get; set; }
		[XmlElement]
		public Object Default { get; set; }
	}
}
