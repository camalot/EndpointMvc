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
using EndpointMvc.Reflection;
using System.ServiceModel;

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
			var reflector = new Reflector ( );

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
					data.Properties.AddRange ( reflector.GetPropertyInfo ( "", prop ) );
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

			var reflector = new Reflector ( );
			var types = reflector.GetTypes ( AppDomain.CurrentDomain );

			foreach ( var type in reflector.GetTypes ( AppDomain.CurrentDomain ) ) {
				var areaName = reflector.FindAreaFromNamespace ( type.Namespace );
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

				if ( !areas.ContainsKey ( areaName ) ) {
					areas.Add ( areaName, currentArea );
				}

				var deprecated = type.GetCustomAttribute<DeprecatedAttribute> ( );
				var obsolete = type.GetCustomAttribute<ObsoleteAttribute> ( );
				var sinceVer = type.GetCustomAttribute<SinceVersionAttribute> ( );
				var auth = type.HasAttribute<RequiresAuthenticationAttribute> ( ) || type.HasAttribute<AuthorizeAttribute> ( );
				var reqHttps = type.HasAttribute<RequireHttpsAttribute> ( );

				var endpoint = type.GetCustomAttribute<EndpointAttribute> ( );
				var serviceContract = type.GetCustomAttribute<ServiceContractAttribute> ( );

				// create the service for the type
				var epService = new EndpointService {
					Properties = reflector.GetCustomProperties(type).ToList(),
					Gists = reflector.GetGists(type).ToList ( )
				};


				epService.Name = reflector.GetName(type);
				epService.Description = reflector.GetDescription(type);
				epService.QualifiedName = "{0}.{1}".With ( areaName, epService.Name );

				// get all the applicable methods for the type
				foreach ( var method in reflector.GetMethods(type) ) {
					var name = reflector.GetName ( method );
					var returnType = reflector.GetReturnType ( method );

					var scheme = reflector.DoesRequireHttps(method) ? "https" : Request.Url.Scheme;
					var actionUrl = GenerateActionUrl ( epService.Name.ToLower ( ), name.ToLower ( ), new { area = areaName.ToLower ( ) }, scheme );
					var methods = reflector.GetHttpVerbs(method);
					var qualifiedKey = "{0}.{1}.{2}_{3}".With ( areaName, epService.Name, name, String.Join ( "_", methods ) );
					var epi = new EndpointInfo ( ) {
						QualifiedName = "{0}.{1}.{2}".With ( areaName, epService.Name, name ),
						Name = name,
						ReturnType = returnType.ToArrayName(),
						QualifiedReturnType = returnType.QualifiedName ( ),
						ContentTypes = reflector.GetContentTypes(method).ToList(),
						Description = reflector.GetDescription(method),
						HttpMethods = reflector.GetHttpVerbs(method).ToList(),
						Params = reflector.GetParams(method).ToList(),
						Url = actionUrl,
						SinceVersion = reflector.GetSinceVersion(method),
						Properties = reflector.GetCustomProperties(method).ToList(),
						Gists = reflector.GetGists(method).ToList()
					};
					try {
						epService.Endpoints.Add ( qualifiedKey, epi );
					} catch ( ArgumentException aex ) {
						throw new ArgumentException ( "{0}: {1} {2}".With ( aex.Message, qualifiedKey, type.FullName ) );
					}
				}
				currentArea.Services.Add ( epService.Name, epService );
			}
			return areas;
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
		

	}
}
