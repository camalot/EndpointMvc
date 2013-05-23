using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
		public String Name { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[XmlElement]
		public String Description { get; set; }
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		[XmlElement]
		public String Url { get; set; }
		/// <summary>
		/// Gets or sets the HTTP methods.
		/// </summary>
		/// <value>
		/// The HTTP methods.
		/// </value>
		[XmlArray("Methods")]
		[XmlArrayItem("Method")]
		public List<String> HttpMethods { get; set; }
		/// <summary>
		/// Gets or sets the params.
		/// </summary>
		/// <value>
		/// The params.
		/// </value>
		[XmlArray("Params")]
		[XmlArrayItem("Param")]
		public List<ParamInfo> Params { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="EndpointInfo" /> is obsolete.
		/// </summary>
		/// <value>
		///   <c>true</c> if obsolete; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		public bool Obsolete { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="EndpointInfo"/> is deprecated.
		/// </summary>
		/// <value>
		///   <c>true</c> if deprecated; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		public bool Deprecated { get; set; }
		/// <summary>
		/// Gets or sets the since version.
		/// </summary>
		/// <value>
		/// The since version.
		/// </value>
		[XmlAttribute]
		public String SinceVersion { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this endpoint requires authentication.
		/// </summary>
		/// <value>
		/// <c>true</c> if this endpoint requires authentication; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		public bool RequiresAuthentication { get; set; }
	}
}
