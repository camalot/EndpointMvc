using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndpointMvc.Models {
	/// <summary>
	/// Parameter information for an endpoint
	/// </summary>
	public class ParamInfo {
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
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Type", Prompt = "Type" )]
		public String Type { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ParamInfo"/> is optional.
		/// </summary>
		/// <value>
		///   <c>true</c> if optional; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute]
		[Display ( Name = "Optional", Prompt = "Optional" )]
		public bool Optional { get; set; }
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
		/// Gets or sets the default.
		/// </summary>
		/// <value>
		/// The default.
		/// </value>
		[XmlElement("Default", IsNullable=true)]
		[Display(Name = "Default", Prompt = "Default")]
		public Object Default { get; set; }
	}
}
