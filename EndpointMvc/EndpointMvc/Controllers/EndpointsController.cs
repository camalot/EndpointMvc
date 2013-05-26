﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Attributes;
using EndpointMvc.Models;
using EndpointMvc.Extensions;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;

namespace EndpointMvc.Controllers {
	public class EndpointsController : Controller {
		public ActionResult Json ( /*String id*/ ) {
			//if ( String.IsNullOrWhiteSpace ( id ) ) {
				var data = BuildEndpointData ( );
				return this.EndpointJson<Dictionary<String, EndpointArea>> ( data );
			/*} else {
				return this.EndpointJson<DefineData> ( GetDefineParamInfo ( id ) );
			}*/
		}

		public ActionResult Xml ( /*String id*/ ) {
			//if ( String.IsNullOrWhiteSpace ( id ) ) {
				var data = BuildEndpointData ( );
				return this.EndpointXml<EndpointData> ( new EndpointData {
					Areas = data.Select ( a => a.Value ).OrderBy ( a => a.Name ).ToList ( )
				} );
			/*} else {
				return this.EndpointXml<DefineData> ( GetDefineParamInfo ( id ) );
			}*/
		}

		public ActionResult Html ( /*String id*/ ) {
			//if ( String.IsNullOrWhiteSpace ( id ) ) {
				var data = BuildEndpointData ( );
				return View ( new EndpointData {
					Areas = data.Select ( a => a.Value ).OrderBy ( a => a.Name ).ToList ( )
				} );
			/*} else {
				var @params = GetDefineParamInfo ( id );
				return View ( "Define", @params );
			}*/
		}

		private DefineData GetDefineParamInfo ( String typeName ) {
			var parts = typeName.Require ( ).Split ( ',' ).Require ( );
			var asm = AppDomain.CurrentDomain.GetAssemblies ( ).FirstOrDefault ( a => a.GetName ( ).Name.Equals ( parts[1].Trim ( ).Replace ( "/", "" ), StringComparison.InvariantCultureIgnoreCase ) );
			if ( asm != null ) {
				var type = asm.GetType ( parts[0].Trim ( ), false, true );
				var data = new DefineData {
					Name = type.Name,
					QualifiedName = type.QualifiedName ( ),
				};
				var props = type.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).Where ( p => p.GetCustomAttribute<IgnoreAttribute> ( ) == null );
				foreach ( var prop in props ) {
					data.Properties.AddRange ( GetPropertyInfo ( "", prop ) );
				}
				return data;
			}
			return null;
		}

		/// <summary>
		/// Builds the endpoints data.
		/// </summary>
		/// <returns></returns>
		private Dictionary<String, EndpointArea> BuildEndpointData ( ) {
			var areaRoute = (String)RouteData.Values["area"];
			var areas = new Dictionary<String, EndpointArea> ( );

			// each assembly, look for custom attribute
			foreach ( var asm in AppDomain.CurrentDomain.GetAssemblies ( ).Where ( a => !a.IsDynamic ) ) {
				// get each type
				foreach ( var type in asm.GetTypes ( ).Where ( t => t.Is<Controller> ( ) && t.GetCustomAttribute<EndpointAttribute> ( ) != null ) ) {
					var areaName = FindAreaFromNamespace ( type.Namespace );
					var currentArea = new EndpointArea {
						Name = areaName,
						QualifiedName = areaName
					};

					if ( areas.ContainsKey ( areaName ) ) {
						currentArea = areas[areaName];
					}

					if ( !String.IsNullOrWhiteSpace ( areaRoute ) && String.Compare ( areaName, areaRoute, true ) != 0 ) {
						continue;
					}

					var endpoint = type.GetCustomAttribute<EndpointAttribute> ( );
					var deprecated = type.GetCustomAttribute<DeprecatedAttribute> ( );
					var obsolete = type.GetCustomAttribute<ObsoleteAttribute> ( );
					var sinceVer = type.GetCustomAttribute<SinceVersionAttribute> ( );
					var auth = type.GetCustomAttribute<RequiresAuthenticationAttribute> ( ) != null || type.GetCustomAttribute<AuthorizeAttribute> ( ) != null;
					var reqHttps = type.GetCustomAttribute<RequireHttpsAttribute> ( ) != null;
					var customProperties = type.GetCustomAttributes<CustomPropertyAttribute> ( );

					if ( endpoint != null ) {
						if ( !areas.ContainsKey ( areaName ) ) {
							areas.Add ( areaName, currentArea );
						}

						var epService = new EndpointService {
							Properties = GetCustomProperties ( customProperties )
						};

						var endpointName = String.IsNullOrWhiteSpace ( endpoint.Name ) ? type.Name : endpoint.Name;
						if ( endpointName.EndsWith ( "Controller" ) ) {
							endpointName = endpointName.Substring ( 0, endpointName.Length - 10 );
						}
						epService.Name = endpointName;

						var epDescription = type.GetCustomAttribute<DescriptionAttribute> ( );
						epService.Description = epDescription == null ? String.Empty : epDescription.Description;
						epService.QualifiedName = "{0}.{1}".With ( areaName, epService.Name );

						type.GetMethods ( ).Where ( m =>
								m.IsPublic &&
								!m.IsSpecialName &&
								!m.ReturnType.Is<Type> ( ) &&
								!m.IsVirtual &&
								m.GetCustomAttribute<NonActionAttribute>() == null &&
								m.GetCustomAttribute<IgnoreAttribute> ( ) == null
							).ForEach ( meth => {
								// get the name of the method
								var name = meth.Name;
								var actionNameAttr = meth.GetCustomAttribute<ActionNameAttribute> ( );
								var methDeprecated = meth.GetCustomAttribute<DeprecatedAttribute> ( );
								var methIbsolete = meth.GetCustomAttribute<ObsoleteAttribute> ( );
								var methSinceVer = meth.GetCustomAttribute<SinceVersionAttribute> ( );
								var methAuth = meth.GetCustomAttribute<RequiresAuthenticationAttribute> ( ) != null || meth.GetCustomAttribute<AuthorizeAttribute> ( ) != null;
								var methReqHttps = meth.GetCustomAttribute<RequireHttpsAttribute> ( ) != null;
								var methCustProps = meth.GetCustomAttributes<CustomPropertyAttribute> ( );
								var methContentTypes = meth.GetCustomAttributes<ContentTypeAttribute> ( ).Select ( m => m.ContentType ).ToList ( );
								var methReturnType = meth.GetCustomAttribute<ReturnTypeAttribute> ( );
								var defaultMethReturnType = meth.ReturnType;

								// get the return type
								var returnType = defaultMethReturnType.Is<ActionResult> ( ) ?
									methReturnType == null ? typeof ( object ) : methReturnType.ReturnType :
									methReturnType == null ? defaultMethReturnType : methReturnType.ReturnType;

								if ( actionNameAttr != null ) {
									name = actionNameAttr.Name;
								}

								// get the description attribute
								var descAttr = meth.GetCustomAttribute<DescriptionAttribute> ( );
								var desc = String.Empty;
								if ( descAttr != null ) {
									desc = descAttr.Description;
								}

								// get the method verbs
								var verbs = GetMethodVerbs ( meth );

								// get the method params
								var paras = GetParams ( meth );

								var scheme = methReqHttps || reqHttps ? "https" : Request.Url.Scheme;
								var actionUrl = GenerateActionUrl ( epService.Name.ToLower ( ), name.ToLower ( ), new { area = areaName.ToLower ( ) }, scheme );

								// info from some of the attributes.
								var actionProperties = new List<PropertyKeyValuePair<String, Object>> {
									new PropertyKeyValuePair<String,object> {
										Key = "Deprecated",
										// get the value from either the method, or the type.
										Value = methDeprecated != null || deprecated != null,
										// get the message from either the method, or the type
										Description = methDeprecated != null ? methDeprecated.Message : 
											deprecated != null ? deprecated.Message : String.Empty
									},
									new PropertyKeyValuePair<String,object> {
										Key = "Obsolete",
										// get the value from either the method, or the type.
										Value = methIbsolete != null || obsolete != null,
										// get the message from either the method, or the type
										Description = methIbsolete != null ? methIbsolete.Message : 
											obsolete != null ? obsolete.Message : String.Empty
									}, new PropertyKeyValuePair<String,object> {
										Key = "Require SSL",
										// get the value from either the method, or the type.
										Value = methReqHttps || reqHttps,
										// get the message from either the method, or the type
										Description = methReqHttps || reqHttps ? "Forces an unsecured HTTP request to be re-sent over HTTPS." : String.Empty
									},
									new PropertyKeyValuePair<String,object> {
										Key = "Require Authorization",
										// get the value from either the method, or the type.
										Value = methAuth || auth,
										// get the message from either the method, or the type
										Description = methAuth || auth ? "Request must be authenticated before access to content is permitted." : String.Empty
									}
								};

								var cust = actionProperties.Union ( GetCustomProperties ( methCustProps ).DefaultIfEmpty ( ).Union ( epService.Properties.DefaultIfEmpty ( ),
									new PropertyKeyValuePairEqualityComparer<String, Object> ( )
								) ).ToList ( );

								var epi = new EndpointInfo ( ) {
									QualifiedName = "{0}.{1}.{2}".With ( areaName, epService.Name, name ),
									Name = name,
									ReturnType = returnType.Name,
									QualifiedReturnType = returnType.QualifiedName ( ),
									ContentTypes = methContentTypes,
									Description = desc,
									HttpMethods = verbs,
									Params = paras,
									Url = actionUrl,
									SinceVersion = sinceVer != null ? sinceVer.Version.ToString ( ) :
										methSinceVer != null ? methSinceVer.Version.ToString ( ) :
										null,
									Properties = cust
								};
								epService.Endpoints.Add ( name, epi );
							} );

						currentArea.Services.Add ( epService.Name, epService );
					}
				}
			}
			return areas;
		}

		/// <summary>
		/// Finds the area from namespace.
		/// </summary>
		/// <param name="namespace">The namespace.</param>
		/// <returns></returns>
		private String FindAreaFromNamespace ( string @namespace ) {
			var m = @namespace.Match ( "\\.(?:Controllers|Areas)\\.([^\\.]+)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			return m.Success ? m.Groups[1].Value : String.Empty;
		}

		/// <summary>
		/// Generates the action URL.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="action">The action.</param>
		/// <param name="routeValues">The route values.</param>
		/// <returns></returns>
		private String GenerateActionUrl ( string controller, string action, object routeValues, String scheme ) {
			return Url.Action ( action, controller, routeValues, scheme );
		}

		/// <summary>
		/// Gets the parameters from the method info.
		/// </summary>
		/// <param name="mi">The method info.</param>
		/// <returns></returns>
		private List<ParamInfo> GetParams ( MethodInfo mi ) {
			var paramList = new List<ParamInfo> ( );

			mi.GetParameters ( ).Where ( p => !p.IsOut ).ForEach ( pi => {
				paramList.AddRange ( GetParamInfo ( String.Empty, pi ) );
			} );

			return paramList;
		}

		/// <summary>
		/// Gets the parameter info.
		/// </summary>
		/// <param name="baseName">Name of the base.</param>
		/// <param name="pi">The property info.</param>
		/// <returns></returns>
		private List<ParamInfo> GetParamInfo ( String baseName, ParameterInfo pi ) {
			var list = new List<ParamInfo> ( );
			var da = pi.GetCustomAttribute<DescriptionAttribute> ( );
			var req = pi.GetCustomAttribute<RequiredAttribute> ( );
			var opt = pi.GetCustomAttribute<OptionalAttribute> ( );
			var customProps = pi.GetCustomAttributes<CustomPropertyAttribute> ( );

			// if the parameter is not a "system" type then we try to break it down.
			if ( pi.ParameterType.Namespace.StartsWith ( "System" ) ) {
				var typeName = pi.ParameterType.IsNullable ( ) ? Nullable.GetUnderlyingType ( pi.ParameterType ).Name :
					pi.ParameterType.Is<IEnumerable> ( ) && pi.ParameterType.IsGenericType ? "{0}[]".With ( pi.ParameterType.GenericTypeArguments[0].Name ) :
					pi.ParameterType.Name;

				list.Add ( new ParamInfo {
					Name = pi.Name.ToCamelCase ( ),
					Type = typeName,
					QualifiedType = pi.ParameterType.QualifiedName ( ),
					Description = da == null ? String.Empty : da.Description,
					Optional = ( pi.IsOptional && req == null ) || opt != null,
					Default = pi.DefaultValue,
					Properties = GetCustomProperties ( customProps )
				} );
			} else {
				// this gets the properties of the parameter that are not ignored
				pi.ParameterType.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).Where ( p => p.GetCustomAttribute<IgnoreAttribute> ( ) == null ).ForEach ( p => {
					list.AddRange ( GetPropertyInfo ( "{0}{2}{1}".With ( baseName, pi.Name.ToCamelCase ( ), String.IsNullOrWhiteSpace ( baseName ) ? "" : "." ), p ) );
				} );
			}
			return list;
		}

		/// <summary>
		/// Gets the property info.
		/// </summary>
		/// <param name="baseName">Name of the base.</param>
		/// <param name="pi">The property info.</param>
		/// <returns></returns>
		private List<ParamInfo> GetPropertyInfo ( String baseName, PropertyInfo pi ) {
			var list = new List<ParamInfo> ( );
			var da = pi.GetCustomAttribute<DescriptionAttribute> ( );
			var req = pi.GetCustomAttribute<RequiredAttribute> ( );
			var opt = pi.GetCustomAttribute<OptionalAttribute> ( );

			if ( pi.PropertyType.Namespace.StartsWith ( "System" ) ) {
				var typeName = pi.PropertyType.IsNullable ( ) ? Nullable.GetUnderlyingType ( pi.PropertyType ).Name :
					pi.PropertyType.Is<IEnumerable> ( ) && pi.PropertyType.IsGenericType ? "{0}[]".With ( pi.PropertyType.GenericTypeArguments[0].Name ) :
					pi.PropertyType.Name;

				list.Add ( new ParamInfo {
					Name = "{0}{2}{1}".With ( baseName, pi.Name.ToCamelCase ( ), String.IsNullOrWhiteSpace ( baseName ) ? "" : "." ),
					Type = typeName,
					QualifiedType = pi.PropertyType.QualifiedName ( ),
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

		private List<PropertyKeyValuePair<String, Object>> GetCustomProperties ( IEnumerable<CustomPropertyAttribute> props ) {
			var skv = new List<PropertyKeyValuePair<String, Object>> ( );
			foreach ( var item in props ) {
				skv.Add ( new PropertyKeyValuePair<string, object> {
					Key = item.Name,
					Value = item.Value,
					Description = item.Description
				} );
			}
			return skv;
		}

		/// <summary>
		/// Gets the method verbs.
		/// </summary>
		/// <param name="mi">The method info.</param>
		/// <returns></returns>
		private List<String> GetMethodVerbs ( MethodInfo mi ) {
			var list = new List<String> ( );
			var verbs = mi.GetCustomAttribute<AcceptVerbsAttribute> ( );
			if ( verbs != null ) {
				list.AddRange ( verbs.Verbs );
				return list;
			}

			var post = mi.GetCustomAttribute<HttpPostAttribute> ( );
			var get = mi.GetCustomAttribute<HttpGetAttribute> ( );
			var options = mi.GetCustomAttribute<HttpOptionsAttribute> ( );
			var head = mi.GetCustomAttribute<HttpHeadAttribute> ( );
			var put = mi.GetCustomAttribute<HttpPutAttribute> ( );
			var delete = mi.GetCustomAttribute<HttpDeleteAttribute> ( );

			if ( post != null ) {
				list.Add ( WebRequestMethods.Http.Post );
			} else if ( put != null ) {
				list.Add ( WebRequestMethods.Http.Put );
			} else if ( delete != null ) {
				list.Add ( "DELETE" );
			} else if ( options != null ) {
				list.Add ( "OPTIONS" );
			} else if ( head != null ) {
				list.Add ( WebRequestMethods.Http.Head );
			} else {
				list.Add ( WebRequestMethods.Http.Get );
			}

			return list;
		}

	}
}
