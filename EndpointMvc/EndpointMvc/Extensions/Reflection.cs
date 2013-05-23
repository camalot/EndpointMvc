using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvc {
		/// <summary>
		/// Determines whether the specified type is nullable.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullable ( this Type type ) {
			return ( type.IsGenericType && type.GetGenericTypeDefinition ( ).Equals ( typeof ( Nullable<> ) ) );
		}

		/*public static T GetCustomAttribute<T> ( this Type type ) where T : Attribute {
			var attr = type.GetCustomAttributes<T> ( ).FirstOrDefault ( );
			return attr;
		}*/

		/// <summary>
		/// Gets member info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="members">The members.</param>
		/// <returns></returns>
		public static IEnumerable<MemberInfo> WithAttribute<T> ( this IEnumerable<MemberInfo> members ) where T : Attribute {
			return members.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets field info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fields">The fields.</param>
		/// <returns></returns>
		public static IEnumerable<FieldInfo> WithAttribute<T> ( this IEnumerable<FieldInfo> fields ) where T : Attribute {
			return fields.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets module info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="modules">The modules.</param>
		/// <returns></returns>
		public static IEnumerable<Module> WithAttribute<T> ( this IEnumerable<Module> modules ) where T : Attribute {
			return modules.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets method info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="methods">The methods.</param>
		/// <returns></returns>
		public static IEnumerable<MethodInfo> WithAttribute<T> ( this IEnumerable<MethodInfo> methods ) where T : Attribute {
			return methods.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets parameter info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public static IEnumerable<ParameterInfo> WithAttribute<T> ( this IEnumerable<ParameterInfo> parameters ) where T : Attribute {
			return parameters.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets property info that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="properties">The properties.</param>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> WithAttribute<T> ( this IEnumerable<PropertyInfo> properties ) where T : Attribute {
			return properties.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets types that have the specified attribute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		public static IEnumerable<Type> WithAttribute<T> ( this IEnumerable<Type> types ) where T : Attribute {
			return types.Where ( m => m.GetCustomAttribute<T> ( ) != default ( T ) );
		}

		/// <summary>
		/// Gets the methods that have the specified return type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static IEnumerable<MethodInfo> GetMethodsOfReturnType<T> ( this Type type ) {
			return GetMethodsOfReturnType<T> ( type, BindingFlags.Instance | BindingFlags.Public );
		}

		/// <summary>
		/// Gets the methods that have the specified return type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <param name="bindingFlags">The binding flags.</param>
		/// <returns></returns>
		public static IEnumerable<MethodInfo> GetMethodsOfReturnType<T> ( this Type type, BindingFlags bindingFlags ) {
			return type.GetMethods ( bindingFlags )
				.Where ( m => m.ReturnType.IsAssignableFrom ( typeof ( T ) ) )
				.Select ( m => m );
		}

		/// <summary>
		/// Gets the types that are assignable from TType.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		public static IEnumerable<Type> GetTypesThatAre<TType> ( this Assembly assembly ) where TType : class {
			return assembly.GetTypes ( ).Where ( t => typeof ( TType ).IsAssignableFrom ( t ) && !t.IsAbstract && !t.IsGenericType && !t.IsInterface && !t.IsEquivalentTo ( typeof ( TType ) ) );
		}

		/// <summary>
		/// Gets the types that are assignable from TType.
		/// </summary>
		/// <typeparam name="TType">The type.</typeparam>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		public static IEnumerable<Type> GetTypesThatAre<TType> ( this AppDomain domain ) where TType : class {
			var types = new List<Type> ( );
			domain.GetAssemblies ( ).ForEach ( a => {
				types.AddRange ( a.GetTypesThatAre<TType> ( ) );
			} );
			return types;
		}

		public static bool Is<TType> ( this TType t ) where TType : class {
			return t.GetType ( ).Is<TType> ( );
		}

		public static bool Is<TType> ( this object t ) {
			return t is TType;
		}

		public static bool Is<TType> ( this Type t ) where TType : class {
			return typeof ( TType ).IsAssignableFrom ( t );
		}


		public static String QualifiedName ( this Type t ) {
			return "{0}, {1}".With ( t.FullName, t.Assembly.GetName ( ).Name );
		}

		public static T CreateInstance<T> ( this Type t ) {
			var result = (T)Activator.CreateInstance ( t );
			return result;
		}

		public static T CreateInstance<T> ( this Type t, params object[] args ) {
			var result = (T)Activator.CreateInstance ( t, args );
			return result;
		}
	}
}
