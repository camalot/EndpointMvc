﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EndpointMvc.Serialization {
	/// <summary>
	/// 
	/// </summary>
	internal static class JsonSerializerFactory {
		/// <summary>
		/// Creates an instance of the JsonSerializer
		/// </summary>
		/// <returns></returns>
		public static JsonSerializer Create ( ) {
			return new JsonSerializer {
				// force camelCase properties
				ContractResolver = new CamelCasePropertyNamesContractResolver ( ),
				ConstructorHandling = ConstructorHandling.Default,
				Formatting = Formatting.None,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				ObjectCreationHandling = ObjectCreationHandling.Auto,
				PreserveReferencesHandling = PreserveReferencesHandling.None,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.None,
				NullValueHandling = NullValueHandling.Ignore
			};
		}
	}
}
