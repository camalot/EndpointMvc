using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndpointMvc.Serialization {
	internal static class XmlSerializerFactory {
		public static XmlSerializer Create<T> ( ) {
			return Create ( typeof ( T ) );
		}

		public static XmlSerializer Create ( Type type ) {
			return new XmlSerializer ( type );
		}
	}
}
