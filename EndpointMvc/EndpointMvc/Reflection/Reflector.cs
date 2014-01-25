using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvc.Extensions;
using EndpointMvc.Models;
using Camalot.Common.Extensions;
using MoreLinq;

namespace EndpointMvc.Reflection {
	/// <summary>
	/// Gets the information for building the documentation using reflection
	/// </summary>
	internal sealed class Reflector {
		/// <summary>
		/// Gets the types from the specified assemblies.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		/// <returns></returns>
		public IEnumerable<Type> GetTypes ( IEnumerable<Assembly> assemblies ) {
			return GetTypes ( assemblies, f => {
				return ( f.IsPublic && ( f.HasAttribute<EndpointAttribute> ( ) || f.HasAttribute<ServiceContractAttribute> ( ) ) && !f.Namespace.StartsWith ( "System." ) );
			} );
		}

		/// <summary>
		/// Gets the types from the specified assemblies.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public IEnumerable<Type> GetTypes ( IEnumerable<Assembly> assemblies, Func<Type, bool> predicate ) {
			return assemblies.SelectMany ( a => a.GetTypes ( ).Where ( predicate ) );
		}

		/// <summary>
		/// Gets the types from the specified app domain.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public IEnumerable<Type> GetTypes ( AppDomain domain, Func<Type, bool> predicate ) {
			return GetTypes ( domain.GetAssemblies ( ).Where ( a => !a.IsDynamic ), predicate );
		}

		/// <summary>
		/// Gets the types from the specified app domain.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		public IEnumerable<Type> GetTypes ( AppDomain domain ) {
			return GetTypes ( domain.GetAssemblies ( ).Where ( a => !a.IsDynamic ) );
		}

		public Type GetTypeFromName ( String name ) {
			return Type.GetType ( name, true, true );
		}

		/// <summary>
		/// Gets the methods for the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public IEnumerable<MethodInfo> GetMethods ( Type type ) {
			var isServiceContract = type.HasAttribute<ServiceContractAttribute> ( );
			return type.GetMethods ( ).Where ( m =>
				m.IsPublic &&
				!m.IsSpecialName &&
					// this ignores GetType method
				!m.ReturnType.Is<Type> ( ) &&
				( ( !m.IsVirtual && type.IsClass && !type.IsAbstract ) || ( m.IsVirtual && type.IsInterface ) ||
					( isServiceContract && m.HasAttribute<OperationContractAttribute>() ) ) &&
				!m.HasAttribute<System.Web.Mvc.NonActionAttribute> ( ) &&
				!m.HasAttribute<System.Web.Http.NonActionAttribute> ( ) &&
				!m.HasAttribute<IgnoreAttribute> ( ) );
		}

		/// <summary>
		/// Gets the methods for the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<MethodInfo> GetMethods<T> ( ) {
			return GetMethods ( typeof ( T ) );
		}

		/// <summary>
		/// Gets the custom attribute from the specified method, or from the declaring type if the method does not have it.
		/// </summary>
		/// <typeparam name="T">The attribute type</typeparam>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public T GetCustomAttribute<T> ( MethodInfo method ) where T : Attribute {
			var parentType = method.DeclaringType;
			var mattr = method.GetCustomAttribute<T> ( );
			var tattr = parentType.GetCustomAttribute<T> ( );
			return mattr ?? tattr;
		}

		/// <summary>
		/// Gets the custom attribute from the specified method, or from the declaring type if the method does not have it.
		/// </summary>
		/// <typeparam name="T">The attribute type</typeparam>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public IEnumerable<T> GetCustomAttributes<T> ( MethodInfo method ) where T : Attribute {
			var parentType = method.DeclaringType;
			var mattr = method.GetCustomAttributes<T> ( );
			var tattr = parentType.GetCustomAttributes<T> ( );
			return mattr ?? tattr;
		}

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public String GetName ( Type type ) {
			var endpoint = type.GetCustomAttribute<EndpointAttribute> ( );
			var serviceContract = type.GetCustomAttribute<ServiceContractAttribute> ( );
			var endpointName = type.Name;
			if ( endpoint != null ) {
				endpointName = String.IsNullOrWhiteSpace ( endpoint.Name ) ? type.Name : endpoint.Name;
				if ( endpointName.EndsWith ( "Controller" ) ) {
					endpointName = endpointName.Substring ( 0, endpointName.Length - 10 );
				}
			} else if ( serviceContract != null ) {
				endpointName = String.IsNullOrWhiteSpace ( serviceContract.Name ) ? type.Name : serviceContract.Name;
				if ( endpointName.EndsWith ( "Service" ) ) {
					endpointName = endpointName.Substring ( 0, endpointName.Length - 7 );
				}
			} else {
				if ( endpointName.EndsWith ( "Hub" ) ) {
					endpointName = endpointName.Substring ( 0, endpointName.Length - 7 );
				}
			}
			return endpointName;
		}

		/// <summary>
		/// Gets the name of the method by looking for ActionName attribute or OperationContract Attribute.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public String GetName ( MethodInfo method ) {
			var httpAction = method.GetCustomAttribute<System.Web.Http.ActionNameAttribute> ( );
			var mvcAction = method.GetCustomAttribute<System.Web.Mvc.ActionNameAttribute> ( );
			var operation = method.GetCustomAttribute<OperationContractAttribute> ( );

			var httpActionName = httpAction == null ? null : httpAction.Name;
			var mvcActionName = mvcAction == null ? null : mvcAction.Name;
			var operationName = operation == null ? null : operation.Name;
			return !String.IsNullOrWhiteSpace ( httpActionName ) ? httpActionName :
				!String.IsNullOrWhiteSpace ( mvcActionName ) ? mvcActionName :
				!String.IsNullOrWhiteSpace ( operationName ) ? operationName :
				method.Name;
		}

		/// <summary>
		/// Gets the custom properties.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public IEnumerable<PropertyKeyValuePair<String, Object>> GetCustomProperties ( Type type ) {
			return type.GetCustomAttributes<CustomPropertyAttribute> ( ).Select ( c => new PropertyKeyValuePair<String, Object> {
				Key = c.Name,
				Value = c.Value,
				Description = c.Description
			} );
		}

		/// <summary>
		/// Gets the custom properties.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public IEnumerable<PropertyKeyValuePair<String, Object>> GetCustomProperties ( MethodInfo method ) {
			var typeCP = GetCustomProperties ( method.DeclaringType );
			var actionCP = this.GetActionProperties ( method );
			var methCP = method.GetCustomAttributes<CustomPropertyAttribute> ( ).Select ( c => new PropertyKeyValuePair<String, Object> {
				Key = c.Name,
				Value = c.Value,
				Description = c.Description
			} );
			var comparer = new PropertyKeyValuePairEqualityComparer<String, Object> ( );
			return actionCP.Union ( methCP.Union ( typeCP, comparer ), comparer ).ToList ( );
		}

		/// <summary>
		/// Gets the custom properties.
		/// </summary>
		/// <param name="pi">The pi.</param>
		/// <returns></returns>
		public IEnumerable<PropertyKeyValuePair<String, Object>> GetCustomProperties ( ParameterInfo pi ) {
			var added = pi.GetCustomAttributes<CustomPropertyAttribute> ( ).Select ( c => new PropertyKeyValuePair<String, Object> {
				Key = c.Name,
				Value = c.Value,
				Description = c.Description
			} ).ToList();
			
			if ( pi.ParameterType.IsEnum ) {
				var nv = new List<KeyValuePair<string, object>> ( );
				var values = Enum.GetValues ( pi.ParameterType );
				foreach ( var val in values ) {
					var name = Enum.GetName ( pi.ParameterType, val );
					nv.Add ( new KeyValuePair<string, object> ( name, (int)val ) );
				}
				var names = String.Join ( ", ", nv.Select ( s => "{0}={1}".With ( s.Key, s.Value ) ) );
				added.Add ( new PropertyKeyValuePair<string, object> {
					Key = "Possible Values",
					Value = String.Join ( ",", names ),
					Description = ""
				} );
			}
			return added;
		}

		/// <summary>
		/// Gets the gists for the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public IEnumerable<Gist> GetGists ( Type type ) {
			return type.GetCustomAttributes<GistAttribute> ( ).Select ( g => new Gist {
				Id = g.GistId,
				Title = g.Title,
				Description = g.Description
			} );
		}

		/// <summary>
		/// Gets the gists for the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public IEnumerable<Gist> GetGists ( MethodInfo method ) {
			return method.GetCustomAttributes<GistAttribute> ( ).Select ( g => new Gist {
				Id = g.GistId,
				Title = g.Title,
				Description = g.Description
			} );
		}

		/// <summary>
		/// Gets the description for the member info.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		/// <returns></returns>
		public String GetDescription ( MemberInfo memberInfo ) {
			var desc = memberInfo.GetCustomAttribute<DescriptionAttribute> ( );
			return desc != null ? desc.Description : String.Empty;
		}

		/// <summary>
		/// Gets the type of the return.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public Type GetReturnType ( MethodInfo method ) {
			var returnTypeAttr = method.GetCustomAttribute<ReturnTypeAttribute> ( );
			var defaultReturnType = method.ReturnType.GetUnderlyingType();
			var returnValue = defaultReturnType.Is<ActionResult> ( ) ?
				returnTypeAttr == null ? typeof ( object ) : returnTypeAttr.ReturnType :
				returnTypeAttr == null ? defaultReturnType : returnTypeAttr.ReturnType;
			return returnValue;
		}

		/// <summary>
		/// Gets the HTTP verbs.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public IEnumerable<String> GetHttpVerbs ( MethodInfo method ) {
			var list = new List<String> ( );
			var verbs = method.GetCustomAttribute<AcceptVerbsAttribute> ( );
			if ( verbs != null && verbs.Verbs.Count > 0 ) {
				list.AddRange ( verbs.Verbs );
				return list;
			}

			var post = method.HasAttribute<HttpPostAttribute> ( );
			var get = method.HasAttribute<HttpGetAttribute> ( );

#if MVC5
			var options = method.HasAttribute<HttpOptionsAttribute> ( );
			var head = method.HasAttribute<HttpHeadAttribute> ( );
#elif MVC4
			var options = method.HasAttribute<HttpOptionsAttribute> ( );
			var head = method.HasAttribute<HttpHeadAttribute> ( );
#elif MVC3
			var options = false;
			var head = false;
#endif

			var put = method.HasAttribute<HttpPutAttribute> ( );
			var delete = method.HasAttribute<HttpDeleteAttribute> ( );

			if ( post ) {
				list.Add ( WebRequestMethods.Http.Post );
			} else if ( put ) {
				list.Add ( WebRequestMethods.Http.Put );
			} else if ( delete ) {
				list.Add ( "DELETE" );
			} else if ( options ) {
				list.Add ( "OPTIONS" );
			} else if ( head ) {
				list.Add ( WebRequestMethods.Http.Head );
			} else {
				list.Add ( WebRequestMethods.Http.Get );
			}

			return list;
		}

		/// <summary>
		/// Gets the content types.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public IEnumerable<String> GetContentTypes ( MethodInfo method ) {
			return method.GetCustomAttributes<ContentTypeAttribute> ( ).Select ( m => m.ContentType );
		}

		/// <summary>
		/// Gets the since version.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public String GetSinceVersion ( Type type ) {
			var sva = type.GetCustomAttribute<SinceVersionAttribute> ( );
			return sva == null ? null : sva.Version.ToString ( );
		}

		/// <summary>
		/// Gets the since version.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public String GetSinceVersion ( MethodInfo method ) {
			var sva = method.GetCustomAttribute<SinceVersionAttribute> ( );
			var tsva = method.DeclaringType.GetCustomAttribute<SinceVersionAttribute> ( );

			return sva != null ? sva.Version.ToString ( ) :
				tsva != null ? tsva.Version.ToString ( ) :
				null;
		}

		/// <summary>
		/// Does the specified method require HTTPS.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public bool DoesRequireHttps ( MethodInfo method ) {
			return method.HasAttribute<RequireHttpsAttribute> ( ) || method.DeclaringType.HasAttribute<RequireHttpsAttribute> ( );
		}

		/// <summary>
		/// Gets the params.
		/// </summary>
		/// <param name="mi">The mi.</param>
		/// <returns></returns>
		public List<ParamInfo> GetParams ( MethodInfo mi ) {
			var paramList = new List<ParamInfo> ( );

			mi.GetParameters ( ).Where ( p => !p.IsOut ).ForEach ( pi => {
				paramList.AddRange ( GetParamInfo ( String.Empty, pi ) );
			} );

			return paramList;
		}

		/// <summary>
		/// Does the specified method require authorization.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public bool DoesRequireAuthorization ( MethodInfo method ) {
			var ra = this.GetCustomAttribute<RequiresAuthenticationAttribute> ( method );
			var aa = this.GetCustomAttribute<AuthorizeAttribute> ( method );
			return ra != null || aa != null;
		}


		/// <summary>
		/// Finds the area from namespace.
		/// </summary>
		/// <param name="namespace">The namespace.</param>
		/// <returns></returns>
		public String FindAreaFromNamespace ( string @namespace ) {
			// hubs added for SignalR
			var m = @namespace.Match ( "\\.(?:Controllers|Areas|Hubs|Services)\\.([^\\.]+)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			return m.Success ? m.Groups[1].Value : String.Empty;
		}

		/// <summary>
		/// Gets the action properties.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		private IEnumerable<PropertyKeyValuePair<String, Object>> GetActionProperties ( MethodInfo method ) {
			var dep = this.GetCustomAttribute<DeprecatedAttribute> ( method );
			var obsolete = this.GetCustomAttribute<ObsoleteAttribute> ( method );
			var sinceVer = this.GetCustomAttribute<SinceVersionAttribute> ( method );
			var reqHttps = this.DoesRequireHttps ( method );
			var reqAuth = this.DoesRequireAuthorization ( method );
			
			var items = new List<PropertyKeyValuePair<String, Object>> {
				new PropertyKeyValuePair<String,object> {
					Key = "Deprecated",
					// get the value from either the method, or the type.
					Value = dep != null,
					// get the message from either the method, or the type
					Description = dep != null ? dep.Message : String.Empty
				},
				new PropertyKeyValuePair<String,object> {
					Key = "Obsolete",
					// get the value from either the method, or the type.
					Value = obsolete != null,
					// get the message from either the method, or the type
					Description = obsolete != null ? obsolete.Message : String.Empty
				}, new PropertyKeyValuePair<String,object> {
					Key = "Require SSL",
					// get the value from either the method, or the type.
					Value = reqHttps,
					// get the message from either the method, or the type
					Description =reqHttps ? "Forces an unsecured HTTP request to be re-sent over HTTPS." : String.Empty
				},
				new PropertyKeyValuePair<String,object> {
					Key = "Require Authorization",
					// get the value from either the method, or the type.
					Value = reqAuth,
					// get the message from either the method, or the type
					Description = reqAuth ? "Request must be authenticated before access to content is permitted." : String.Empty
				}
				
			};

			if ( sinceVer != null ) {
				items.Add(new PropertyKeyValuePair<String,Object> {
					Key = "Since Version",
					Value = sinceVer.Version.ToString()
				});
			}
			return items;
		}

		/// <summary>
		/// Gets the parameter info.
		/// </summary>
		/// <param name="baseName">Name of the base.</param>
		/// <param name="pi">The property info.</param>
		/// <returns></returns>
		private List<ParamInfo> GetParamInfo ( String baseName, ParameterInfo pi ) {
			var list = new List<ParamInfo> ( );
			var desc = pi.GetCustomAttributeValue<DescriptionAttribute, String> ( x => x.Description).Or(string.Empty);
			var req = pi.GetCustomAttribute<RequiredAttribute> ( ) != null;
			var opt = pi.GetCustomAttribute<OptionalAttribute> ( ) != null;
			var isNullable = pi.ParameterType.IsNullable();
			var customProps = pi.GetCustomAttributes<CustomPropertyAttribute> ( );

			// if the parameter is not a "system" type then we try to break it down.
			if ( pi.ParameterType.Namespace.StartsWith ( "System" ) || pi.ParameterType.IsEnum ) {
				var typeName = GetParameterTypeName(pi.ParameterType);
				list.Add ( new ParamInfo {
					IsSystemType = pi.ParameterType.GetUnderlyingType ( ).IsSystemType ( ),
					Name = pi.Name.ToCamelCase ( ),
					Type = typeName,
					QualifiedType = pi.ParameterType.QualifiedName ( ),
					QualifiedUnderlyingType = pi.ParameterType.GetUnderlyingType().QualifiedName().Replace("[]",""),
					Description = desc,
					Optional = ( pi.IsOptional && !req ) || (isNullable && !req ) || opt,
					Default = pi.DefaultValue,
					Properties = this.GetCustomProperties ( pi ).ToList ( )
				} );
			} else {
				// this gets the properties of the parameter that are not ignored
				pi.ParameterType.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).Where ( p => p.GetCustomAttribute<IgnoreAttribute> ( ) == null ).ForEach ( p => {
					list.AddRange ( GetPropertyInfo ( "{0}{2}{1}".With ( baseName, pi.Name.ToCamelCase ( ), String.IsNullOrWhiteSpace ( baseName ) ? "" : "." ), p ) );
				} );
			}
			return list;
		}

		private String GetParameterTypeName ( Type type ) {
			if ( type.IsNullable ( ) ) {
				// if nullable, we get the actual type
				return GetParameterTypeName ( Nullable.GetUnderlyingType ( type ) );
			}

			if ( type.Is<IEnumerable> ( ) && type.IsGenericType ) {
				return type.ToArrayName ( );
			}

			if ( type.IsEnum ) {
				return "String|Int32";
			}

			return type.Name;
		}

		public List<EnumInfo> GetEnumInfo ( Type @enum ) {
			var list = new List<EnumInfo> ( );
			var desc = @enum.GetCustomAttributeValue<DescriptionAttribute, String> ( x => x.Description ).Or(String.Empty);

			var values = Enum.GetValues ( @enum );
			foreach ( var val in values ) {
				var name = Enum.GetName ( @enum, val );
				var ei = new EnumInfo {
					Name = name,
					Value = (int)val,
					Description = desc,
					Type = typeof ( int ).Name,
					QualifiedType = typeof ( int ).QualifiedName ( ),
				};
				list.Add ( ei );
			}
			return list;
		}

		/// <summary>
		/// Gets the property info.
		/// </summary>
		/// <param name="baseName">Name of the base.</param>
		/// <param name="pi">The property info.</param>
		/// <returns></returns>
		public List<ParamInfo> GetPropertyInfo ( String baseName, PropertyInfo pi ) {
			var list = new List<ParamInfo> ( );
			var da = pi.GetCustomAttribute<DescriptionAttribute> ( );
			var req = pi.GetCustomAttribute<RequiredAttribute> ( );
			var opt = pi.GetCustomAttribute<OptionalAttribute> ( );

			if ( pi.PropertyType.Namespace.StartsWith ( "System" ) ) {
				var typeName = pi.PropertyType.IsNullable ( ) ? Nullable.GetUnderlyingType ( pi.PropertyType ).Name :
					pi.PropertyType.Is<IEnumerable> ( ) && pi.PropertyType.IsGenericType ? "{0}[]".With ( pi.PropertyType.GenericTypeArguments[0].Name ) :
					pi.PropertyType.Name;

				list.Add ( new ParamInfo {
					IsSystemType = pi.PropertyType.GetUnderlyingType().IsSystemType ( ),
					Name = "{0}{2}{1}".With ( baseName, pi.Name.ToCamelCase ( ), String.IsNullOrWhiteSpace ( baseName ) ? "" : "." ),
					Type = typeName,
					QualifiedType = pi.PropertyType.QualifiedName ( ),
					QualifiedUnderlyingType = pi.PropertyType.GetUnderlyingType ( ).QualifiedName ( ).Replace ( "[]", "" ),
					Description = da == null ? String.Empty : da.Description,
					Optional = req == null || opt != null,
					Default = null
				} );
			} else {
				pi.PropertyType.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).ForEach ( p => {
					list.AddRange ( GetPropertyInfo ( "{0}.{1}".With ( baseName, pi.Name.ToCamelCase ( ) ), p ) );
				} );
			}
			return list;
		}

	}
}
