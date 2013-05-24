using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndpointMvc.Exceptions;

namespace EndpointMvc.Extensions {
	/// <summary>
	/// 
	/// </summary>
	public static partial class EndpointMvcExtensions {
		/// <summary>
		/// Makes the specified object required.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Object Require ( this Object obj ) {
			if ( obj == null ) {
				throw new MissingRequiredValueException ( );
			}
			return obj;
		}

		/// <summary>
		/// Makes the specified object required.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Object Require ( this Object obj, String message ) {
			if ( obj == null ) {
				throw new MissingRequiredValueException ( message );
			}
			return obj;
		}

		/// <summary>
		/// Determines whether the specified GUID is empty.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <returns>
		///   <c>true</c> if the specified GUID is empty; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEmpty ( this Guid guid ) {
			return guid == Guid.Empty;
		}

		/// <summary>
		/// Requires the specified GUID.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Guid Require ( this Guid guid ) {
			if ( guid == Guid.Empty ) {
				throw new MissingRequiredValueException ( );
			}
			return guid;
		}

		/// <summary>
		/// Requires the specified GUID.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Guid Require ( this Guid guid, String message ) {
			if ( guid == null ) {
				throw new MissingRequiredValueException ( message );
			}
			return guid;
		}

		/// <summary>
		/// Makes the specified object of <c>T</c> required.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s">The object.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static T Require<T> ( this T s, String message ) {
			if ( s.Equals ( default ( T ) ) ) {
				throw new MissingRequiredValueException ( message );
			}
			return s;
		}

		/// <summary>
		/// Makes the specified object of <c>T</c> required.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s">The object.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static T Require<T> ( this T s ) {
			if ( s.Equals ( default ( T ) ) ) {
				throw new MissingRequiredValueException ( );
			}
			return s;
		}

		/// <summary>
		/// Requires the specified string to have a value.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static String Require ( this String s, String message ) {
			if ( String.IsNullOrWhiteSpace ( s ) ) {
				throw new MissingRequiredValueException ( message );
			}
			return s;
		}
		/// <summary>
		/// Requires the specified string to have a value.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static String Require ( this String s ) {
			if ( String.IsNullOrWhiteSpace ( s ) ) {
				throw new MissingRequiredValueException ( );
			}
			return s;
		}

		/// <summary>
		/// Requires the specified nullable object of <c>T</c> to have a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s">The nullable object.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Nullable<T> Require<T> ( this Nullable<T> s, String message ) where T : struct, IComparable {
			if ( !s.HasValue ) {
				throw new MissingRequiredValueException ( message );
			}
			return s;
		}
		/// <summary>
		/// Requires the specified nullable object of <c>T</c> to have a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s">The nullable object.</param>
		/// <returns></returns>
		/// <exception cref="EndpointMvcExtensions.Exceptions.MissingRequiredValueException"></exception>
		public static Nullable<T> Require<T> ( this Nullable<T> s ) where T : struct, IComparable {
			if ( !s.HasValue ) {
				throw new MissingRequiredValueException ( );
			}
			return s;
		}

		#region RequirePositive/Negative
		/// <summary>
		/// Requires integer to be a positive value.
		/// </summary>
		/// <param name="i">The integer.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static int RequirePositive ( this int i, String param ) {
			return RequireBetween ( i, 0, int.MaxValue, param );
		}

		/// <summary>
		/// Requires integer to be a positive value.
		/// </summary>
		/// <param name="i">The integer.</param>
		/// <returns></returns>
		public static int RequirePositive ( this int i ) {
			return RequireBetween ( i, 0, int.MaxValue );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static short RequirePositive ( this short i, String param ) {
			return RequireBetween ( i, (short)0, short.MaxValue, param );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static short RequirePositive ( this short i ) {
			return RequireBetween ( i, (short)0, short.MaxValue );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static long RequirePositive ( this long i, String param ) {
			return RequireBetween ( i, (long)0, long.MaxValue, param );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static long RequirePositive ( this long i ) {
			return RequireBetween ( i, (long)0, long.MaxValue );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static double RequirePositive ( this double i, String param ) {
			return RequireBetween ( i, (double)0, double.MaxValue, param );
		}

		/// <summary>
		/// Requires number to be a positive value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static double RequirePositive ( this double i ) {
			return RequireBetween ( i, (double)0, double.MaxValue );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static int RequireNegative ( this int i, String param ) {
			return RequireBetween ( i, int.MinValue, 0, param );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static int RequireNegative ( this int i ) {
			return RequireBetween ( i, int.MinValue, 0 );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static short RequireNegative ( this short i, String param ) {
			return RequireBetween ( i, short.MinValue, (short)0, param );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static short RequireNegative ( this short i ) {
			return RequireBetween ( i, short.MinValue, (short)0 );
		}


		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static long RequireNegative ( this long i, String param ) {
			return RequireBetween ( i, long.MinValue, (long)0, param );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static long RequireNegative ( this long i ) {
			return RequireBetween ( i, long.MinValue, (long)0 );
		}


		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		public static double RequireNegative ( this double i, String param ) {
			return RequireBetween ( i, double.MinValue, (double)0, param );
		}

		/// <summary>
		/// Requires number to be a negative value.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <returns></returns>
		public static double RequireNegative ( this double i ) {
			return RequireBetween ( i, double.MinValue, (double)0 );
		}
		#endregion

		#region RequireBetween
		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static int RequireBetween ( this int i, int low, int high, String param ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ), param );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static int RequireBetween ( this int i, int low, int high ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ) );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static short RequireBetween ( this short i, short low, short high, String param ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ), param );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static short RequireBetween ( this short i, short low, short high ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ) );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static long RequireBetween ( this long i, long low, long high, String param ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ), param );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static long RequireBetween ( this long i, long low, long high ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ) );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static double RequireBetween ( this double i, double low, double high, String param ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ), param );
			}
			return i;
		}

		/// <summary>
		/// Requires the number to be between the low and high values.
		/// </summary>
		/// <param name="i">The number.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">value '{2}' must be a value between {0} and {1}.With ( low, high, i )</exception>
		public static double RequireBetween ( this double i, double low, double high ) {
			if ( i <= low || i >= high ) {
				throw new ArgumentException ( "value '{2}' must be a value between {0} and {1}".With ( low, high, i ) );
			}
			return i;
		}
		#endregion
	}
}
