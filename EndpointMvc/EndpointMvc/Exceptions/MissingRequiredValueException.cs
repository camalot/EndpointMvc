using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Exceptions {
	/// <summary>
	/// A required value was not supplied
	/// </summary>
	public class MissingRequiredValueException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="MissingRequiredValueException"/> class.
		/// </summary>
		public MissingRequiredValueException ( )
			: this ( "The required value is missing" ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingRequiredValueException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public MissingRequiredValueException ( String message )
			: base ( message ) {
		}
	}
}
