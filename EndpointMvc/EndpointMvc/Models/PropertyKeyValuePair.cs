using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Models {
	[Serializable]
	public class PropertyKeyValuePair<K,V> {
		public PropertyKeyValuePair ( ) {
			
		}
		public K Key { get; set; }
		public V Value { get; set; }
	}

	internal sealed class PropertyKeyValuePairEqualityComparer<K, V> : IEqualityComparer<PropertyKeyValuePair<K, V>> {

		#region IEqualityComparer<PropertyKeyValuePair<K,V>> Members

		public bool Equals ( PropertyKeyValuePair<K, V> x, PropertyKeyValuePair<K, V> y ) {
			return x == null ? x == y : x.Key.Equals ( y.Key );
		}

		public int GetHashCode ( PropertyKeyValuePair<K, V> obj ) {
			return obj == null ? 0 : obj.Key.GetHashCode ( );
		}

		#endregion
	}
}
