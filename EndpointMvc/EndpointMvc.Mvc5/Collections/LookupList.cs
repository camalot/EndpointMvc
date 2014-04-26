using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Collections {
	public class LookupList<TKey, TValue> : List<KeyValuePair<TKey,TValue>> {

		public void Add(TKey key, TValue value) {
			base.Add(new KeyValuePair<TKey, TValue>(key,value));
		}

	}
}
