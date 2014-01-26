using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EndpointMvc.Models {
	public class EnumInfo {
		public EnumInfo ( ) {
			Properties = new List<PropertyKeyValuePair<string, object>> ( );
		}
		[XmlAttribute]
		[Display ( Name = "Name", Prompt = "Name" )]
		public String Name { get; set; }

		[XmlAttribute]
		[Display ( Name = "Value", Prompt = "Value" )]
		public int Value { get; set; }

		[XmlAttribute]
		[Display ( Name = "Type", Prompt = "Type" )]
		public String Type { get; set; }

		[XmlAttribute]
		[Display ( Name = "QualifiedType", Prompt = "QualifiedType" )]
		public String QualifiedType { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[XmlElement]
		[Display ( Name = "Description", Prompt = "Description" )]
		public String Description { get; set; }

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		/// <value>
		/// The properties.
		/// </value>
		[XmlArray ( "Properties" )]
		[XmlArrayItem ( "Property" )]
		public List<PropertyKeyValuePair<String, Object>> Properties { get; set; }
	}
}
