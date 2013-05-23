using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvc {

		/// <summary>
		/// Iterates over each item of <c>T</c>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumeration">The enumeration.</param>
		/// <param name="action">The action.</param>
		public static void ForEach<T> ( this IEnumerable<T> source, Action<T> action ) {
			action.Require ( );

			foreach ( T item in source.Require() ) {
				action ( item );
			}
		}
	}
}
