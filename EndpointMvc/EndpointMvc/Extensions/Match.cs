using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvc {
		/// <summary>
		/// Iterates over the matches in the Match
		/// </summary>
		/// <param name="m">The m.</param>
		/// <param name="action">The action.</param>
		public static void ForEach ( this Match m, Action<Match> action ) {
			while ( m.Success ) {
				action ( m );
				m = m.NextMatch ( );
			}
		}
	}
}
