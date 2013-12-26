using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Models {
	public class DefineData {
		public DefineData ( ) {
			Properties = new List<ParamInfo> ( );
			Fields = new List<EnumInfo> ( );
		}
		public String Name { get; set; }
		public String QualifiedName { get; set; }
		public List<ParamInfo> Properties { get; set; }

		public List<EnumInfo> Fields { get; set; }
	}
}
