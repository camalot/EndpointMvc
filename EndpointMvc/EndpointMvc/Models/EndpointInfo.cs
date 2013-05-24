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
		/// Gets or sets a value indicating whether this <see cref="EndpointInfo" /> is obsolete.
		/// </summary>
		/// <value>
		///   <c>true</c> if obsolete; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Obsolete", Prompt = "Obsolete" )]
		public bool Obsolete { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="EndpointInfo"/> is deprecated.
		/// </summary>
		/// <value>
		///   <c>true</c> if deprecated; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Deprecated", Prompt = "Deprecated" )]
		public bool Deprecated { get; set; }
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
		/// Gets or sets a value indicating whether this endpoint requires authentication.
		/// </summary>
		/// <value>
		/// <c>true</c> if this endpoint requires authentication; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Requires Authentication", Prompt = "Requires Authentication" )]
		public bool RequiresAuthentication { get; set; }

		[XmlAttribute]
		[Display( Name = "Require SSL", Prompt = "Require SSL" )]
		[JsonProperty("requireSSL")]
		public bool RequireSsl { get; set; }

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
