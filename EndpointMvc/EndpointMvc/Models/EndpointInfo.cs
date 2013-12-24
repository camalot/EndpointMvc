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
	/// 
	/// </summary>
	public class EndpointInfo {
		public EndpointInfo ( ) {
			Properties = new List<PropertyKeyValuePair<string, object>> ( );
			Gists = new List<Gist> ( );
		}

		[XmlIgnore]
		[JsonIgnore]
		public bool IsSystemType { get; set; }

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
		[XmlElement]
		[Display ( Name = "Description", Prompt = "Description" )]
		public String Description { get; set; }
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		[XmlElement]
		[Display ( Name = "Url", Prompt = "Url" )]
		public String Url { get; set; }
		/// <summary>
		/// Gets or sets the HTTP methods.
		/// </summary>
		/// <value>
		/// The HTTP methods.
		/// </value>
		[XmlArray("Methods")]
		[XmlArrayItem("Method")]
		[Display ( Name = "Methods", Prompt = "Methods" )]
		public List<String> HttpMethods { get; set; }
		/// <summary>
		/// Gets or sets the params.
		/// </summary>
		/// <value>
		/// The params.
		/// </value>
		[XmlArray("Parameters")]
		[XmlArrayItem("Parameter")]
		[Display ( Name = "Parameters", Prompt = "Parameters" )]
		public List<ParamInfo> Params { get; set; }
		/// <summary>
		/// Gets or sets the since version.
		/// </summary>
		/// <value>
		/// The since version.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Since Version", Prompt = "Since Version" )]
		public String SinceVersion { get; set; }

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		/// <value>
		/// The properties.
		/// </value>
		[XmlArray ( "Properties" )]
		[XmlArrayItem ( "Property" )]
		public List<PropertyKeyValuePair<String, Object>> Properties { get; set; }

		[XmlArray ( "Examples" )]
		[XmlArrayItem ( "Gist" )]
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

		[XmlAttribute]
		public String ReturnType { get; set; }
		[XmlAttribute]
		[Display ( Name = "QualifiedReturnType", Prompt = "QualifiedReturnType" )]
		public String QualifiedReturnType { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public String QualifiedUnderlyingReturnType { get; set; }

		[XmlArray("ContentTypes")]
		[XmlArrayItem("ContentType")]
		public List<String> ContentTypes { get; set; }

	}
}
