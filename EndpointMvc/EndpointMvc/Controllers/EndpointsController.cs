using System;
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

		public ActionResult Json ( ) {
			var data = BuildEndpointData ( );
			return this.EndpointJson<Dictionary<String, EndpointArea>> ( data );
		}

		public ActionResult Xml ( ) {
			var data = BuildEndpointData ( );
			return this.EndpointXml<EndpointData> ( new EndpointData {
				Areas = data.Select ( a => a.Value ).ToList()
			} );
		}

		public ActionResult Html ( ) {
			var data = BuildEndpointData ( );
			return View ( new EndpointData {
				Areas = data.Select ( a => a.Value ).ToList()
			} );
		}

		private Dictionary<String, EndpointArea> BuildEndpointData ( ) {
			var areaRoute = (String)RouteData.Values["area"];
			var areas = new Dictionary<String, EndpointArea> ( );
			var assemblies = AppDomain.CurrentDomain.GetAssemblies ( );

			// each assembly, look for custom attribute
			foreach ( var asm in assemblies ) {
				// get each type
				foreach ( var type in asm.GetTypes ( ).Where ( t => t.Is<Controller> ( ) && t.GetCustomAttribute<EndpointAttribute> ( ) != null ) ) {
					var areaName = FindAreaFromNamespace ( type.Namespace );
					var carea = new EndpointArea {
						Name = areaName
					};
					if ( areas.ContainsKey ( areaName ) ) {
						carea = areas[areaName];
					}

					if ( !String.IsNullOrWhiteSpace ( areaRoute ) && String.Compare ( areaName, areaRoute, true ) != 0 ) {
						continue;
					}

					var endpoint = type.GetCustomAttribute<EndpointAttribute> ( );
					var deprecated = type.GetCustomAttribute<DeprecatedAttribute> ( );
					var obsolete = type.GetCustomAttribute<ObsoleteAttribute> ( );
					var sinceVer = type.GetCustomAttribute<SinceVersionAttribute> ( );
					var auth = type.GetCustomAttribute<RequiresAuthenticationAttribute> ( );

					if ( endpoint != null ) {
						if ( !areas.ContainsKey ( areaName ) ) {
							areas.Add ( areaName, carea );
						}

						var epService = new EndpointService ( );
						var epn = String.IsNullOrWhiteSpace ( endpoint.Name ) ? type.Name : endpoint.Name;
						if ( epn.EndsWith ( "Controller" ) ) {
							epn = epn.Substring ( 0, epn.Length - 10 );
						}
						epService.Name = epn;
						var epDa = type.GetCustomAttribute<DescriptionAttribute> ( );
						epService.Description = epDa == null ? String.Empty : epDa.Description;
						//var qualifiedEpKey = "{0}.{1}".With ( areaName, epService.Name );

						type.GetMethods ( ).Where ( m =>
								m.IsPublic &&
								!m.IsSpecialName &&
								( m.ReturnType.Is<ActionResult> ( ) || m.ReturnType.IsPrimitive ) &&
								!m.IsVirtual &&
								m.GetCustomAttribute<IgnoreAttribute> ( ) == null
							).ForEach ( meth => {
								// get the name of the method
								var name = meth.Name;
								var ana = meth.GetCustomAttribute<ActionNameAttribute> ( );
								var mdep = meth.GetCustomAttribute<DeprecatedAttribute> ( );
								var mobsolete = meth.GetCustomAttribute<ObsoleteAttribute> ( );
								var msv = meth.GetCustomAttribute<SinceVersionAttribute> ( );
								var mauth = meth.GetCustomAttribute<RequiresAuthenticationAttribute> ( );

								if ( ana != null ) {
									name = ana.Name;
								}
								var da = meth.GetCustomAttribute<DescriptionAttribute> ( );
								var desc = String.Empty;
								if ( da != null ) {
									desc = da.Description;
								}
								var vbs = GetMethodVerbs ( meth );
								var paras = GetParams ( meth );

								var actionUrl = GenerateActionUrl ( epService.Name.ToLower ( ), name.ToLower ( ), new { area = areaName.ToLower ( ) } );

								// this is a fully qualified name so there can be endpoints with the same name
								//var qualifiedEpiKey = "{0}.{1}.{2}".With ( areaName, epService.Name, name );

								var epi = new EndpointInfo ( ) {
									Name = name,
									Description = desc,
									HttpMethods = vbs,
									Params = paras,
									Url = actionUrl,
									Deprecated = mdep != null || deprecated != null,
									Obsolete = mobsolete != null || obsolete != null,
									RequiresAuthentication = auth != null | mauth != null,
									SinceVersion = sinceVer != null ? sinceVer.Version.ToString ( ) :
										msv != null ? msv.Version.ToString ( ) :
										null
								};
								epService.Endpoints.Add ( name, epi );
							} );

						carea.Services.Add ( epService.Name, epService );
					}
				}
			}
			return areas;
		}

		private String FindAreaFromNamespace ( string @namespace ) {
			var m = @namespace.Match ( "\\.(?:Controllers|Areas)\\.([^\\.]+)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			return m.Success ? m.Groups[1].Value : String.Empty;
		}

		private String GenerateActionUrl ( string controller, string action, object routeValues ) {
			return Url.Action ( action, controller, routeValues, Request.Url.Scheme );
		}

		private List<ParamInfo> GetParams ( MethodInfo mi ) {
			var paramList = new List<ParamInfo> ( );

			mi.GetParameters ( ).Where ( p => !p.IsOut ).ForEach ( pi => {
				paramList.AddRange ( GetParamInfo ( String.Empty, pi ) );
			} );

			return paramList;
		}

		private List<ParamInfo> GetParamInfo ( String baseName, ParameterInfo pi ) {
			var list = new List<ParamInfo> ( );
			var da = pi.GetCustomAttribute<DescriptionAttribute> ( );
			var req = pi.GetCustomAttribute<RequiredAttribute> ( );
			var opt = pi.GetCustomAttribute<OptionalAttribute> ( );

			// if the parameter is not a "system" type then we try to break it down.
			if ( pi.ParameterType.Namespace.StartsWith ( "System" ) ) {
				var typeName = pi.ParameterType.IsNullable ( ) ? Nullable.GetUnderlyingType ( pi.ParameterType ).Name :
					pi.ParameterType.Is<IEnumerable> ( ) && pi.ParameterType.IsGenericType ? "{0}[]".With ( pi.ParameterType.GenericTypeArguments[0].Name ) :
					pi.ParameterType.Name;

				list.Add ( new ParamInfo {
					Name = pi.Name.ToCamelCase ( ),
					Type = typeName,
					Description = da == null ? String.Empty : da.Description,
					Optional = ( pi.IsOptional && req == null ) || opt != null,
					Default = pi.DefaultValue
				} );
			} else {
				// this gets the properties of the parameter that are not ignored
				pi.ParameterType.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).Where ( p => p.GetCustomAttribute<IgnoreAttribute> ( ) == null ).ForEach ( p => {
					list.AddRange ( GetPropertyInfo ( "{0}{2}{1}".With ( baseName, pi.Name.ToCamelCase ( ), String.IsNullOrWhiteSpace ( baseName ) ? "" : "." ), p ) );
				} );
			}
			return list;
		}

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
