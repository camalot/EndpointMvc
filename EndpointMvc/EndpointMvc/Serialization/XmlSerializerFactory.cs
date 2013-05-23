using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndpointMvc.Serialization {
	/// <summary>
	/// 
	/// </summary>
	internal static class XmlSerializerFactory {
		/// <summary>
		/// Creates an instance of the Xml Serializer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static XmlSerializer Create<T> ( ) {
			return Create ( typeof ( T ) );
		}

		/// <summary>
		/// Creates an instance of the Xml Serializer
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static XmlSerializer Create ( Type type ) {
			return new XmlSerializer ( type, new Type[] { typeof(System.DBNull) } );
		}
	}
}
