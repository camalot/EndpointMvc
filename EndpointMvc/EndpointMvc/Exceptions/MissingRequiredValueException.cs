using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Exceptions {
	public class MissingRequiredValueException : Exception {
		public MissingRequiredValueException ( )
			: this ( "The required value is missing" ) {
		}

		public MissingRequiredValueException ( String message )
			: base ( message ) {
		}
	}
}
