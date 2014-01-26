using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {
		public static String ToArrayName ( this Type type ) {
			var lowType = type;
			if ( type.IsAwaitableTask ( ) ) {
				lowType = type.GenericTypeArguments[0];
			}

			return lowType.Is<IEnumerable> ( ) && lowType.IsGenericType ?
				"{0}[]".With ( lowType.GenericTypeArguments[0].Name ) :
				lowType.Name;
		}

		/// <summary>
		/// Determines whether the specified type an awaitable task.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static bool IsAwaitableTask ( this Type type ) {
			return ( type.IsGenericType && type.GetGenericTypeDefinition ( ).Is<Task> ( ) );
		}

		/// <summary>
		/// Gets the type of the underlying.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static Type GetUnderlyingType ( this Type type ) {
			var lowType = type;
			if ( type.IsAwaitableTask ( ) ) {
				lowType = type.GenericTypeArguments[0];
			}

			return lowType.IsGenericType && lowType.GenericTypeArguments.Length == 1 ?
				lowType.GenericTypeArguments[0] :
				lowType;
		}

		/// <summary>
		/// Determines whether the specified type is a system type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static bool IsSystemType ( this Type type ) {
			return type.FullName.Equals ( "System" ) || type.FullName.StartsWith ( "System." );
		}
	}
}
