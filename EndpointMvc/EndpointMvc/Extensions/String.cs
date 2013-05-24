using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {

		/// <summary>
		/// Applies the arguments to the supplied format string
		/// </summary>
		/// <param name="s">The format string.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public static String With ( this String s, params object[] args ) {
			return String.Format ( s, args );
		}

		/// <summary>
		/// Gets the matches for the string to the specified pattern
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <returns></returns>
		public static Match Match ( this String s, String pattern ) {
			return Match ( s, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
		}

		/// <summary>
		/// Gets the matches for the string to the specified pattern
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static Match Match ( this String s, String pattern, RegexOptions options ) {
			return Regex.Match ( s, pattern, options );
		}


		/// <summary>
		/// Determines whether the string is match to the specified pattern.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <returns>
		///   <c>true</c> if the string is match; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsMatch ( this String s, String pattern ) {
			return IsMatch ( s, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
		}

		/// <summary>
		/// Determines whether the string is match to the specified pattern.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The options.</param>
		/// <returns>
		///   <c>true</c> if the string is match; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsMatch ( this String s, String pattern, RegexOptions options ) {
			return Regex.IsMatch ( s, pattern, options );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replacement">The replacement.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static String Replace ( this String s, String pattern, String replacement, RegexOptions options ) {
			return REReplace ( s, pattern, replacement, options );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static String Replace ( this String s, String pattern, MatchEvaluator evaluator ) {
			return Regex.Replace ( s, pattern, evaluator );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The s.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The options.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static String Replace ( this String s, String pattern, RegexOptions options, MatchEvaluator evaluator ) {
			return Regex.Replace ( s, pattern, evaluator, options );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replacement">The replacement.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static String REReplace ( this String s, String pattern, String replacement, RegexOptions options ) {
			return Regex.Replace ( s, pattern, replacement, options );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replacement">The replacement.</param>
		/// <returns></returns>
		public static String REReplace ( this String s, String pattern, String replacement ) {
			return REReplace ( s, pattern, replacement, RegexOptions.Compiled | RegexOptions.IgnoreCase );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The options.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static String REReplace ( this String s, String pattern, RegexOptions options, MatchEvaluator evaluator ) {
			return Regex.Replace ( s, pattern, evaluator, options );
		}

		/// <summary>
		/// Performs a regex replace
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static String REReplace ( this String s, String pattern, MatchEvaluator evaluator ) {
			return REReplace ( s, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, evaluator );
		}


		/// <summary>
		/// Gets the bytes from the supplied string.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public static byte[] GetBytes ( this String s, Encoding encoding ) {
			return encoding.GetBytes ( s );
		}

		/// <summary>
		/// Gets the bytes from the supplied string.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns></returns>
		public static byte[] GetBytes ( this String s ) {
			return GetBytes ( s, Encoding.Unicode );
		}

		/// <summary>
		/// Gets the string from the supplied bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public static String GetString ( this byte[] bytes, Encoding encoding ) {
			return encoding.GetString ( bytes );
		}

		/// <summary>
		/// Gets the string from the supplied bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <returns></returns>
		public static String GetString ( this byte[] bytes ) {
			return GetString ( bytes, Encoding.Unicode );
		}

		/// <summary>
		/// Converts the objects ToString method to camel case
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static String ToCamelCase ( this object input ) {
			return input.ToString ( ).ToCamelCase ( );
		}

		/// <summary>
		/// Converts the string to camel case
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static String ToCamelCase ( this String input ) {
			return input.Require ( "'input' cannot be null or empty." ).Replace ( input[0], input.ToLower ( )[0] );
		}

		/// <summary>
		/// Converts the string to pascal case
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static String ToPascalCase ( this String input ) {
			return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase ( input.Require ( "'input' cannot be null or empty." ) );
		}

		public static String StripHtml ( this string s ) {
			if ( String.IsNullOrWhiteSpace ( s ) ) {
				return s;
			}
			var options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline;
			return s.Replace ( "\\<br\\s*/?\\>", Environment.NewLine, options ).Replace ( "<[^>]+>||&\\#\\d{1,};", String.Empty, options );
		}

	}
}
