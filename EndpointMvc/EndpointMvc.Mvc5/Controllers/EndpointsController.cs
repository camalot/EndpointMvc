using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Routing;
using Camalot.Common.Extensions;
using Camalot.Common.Mvc.Extensions;
using EndpointMvc.Attributes;
using EndpointMvc.Extensions;
using EndpointMvc.Models;
using EndpointMvc.Reflection;
using MoreLinq;

namespace EndpointMvc.Controllers {
	public class EndpointsController : Controller {
		public JsonResult Json() {
			var data = BuildEndpointData();
			return this.JSON(data);
		}

		public ActionResult Xml() {
			var data = BuildEndpointData();
			return this.XML(new EndpointData {
				Areas = data.Select(a => a.Value).OrderBy(a => a.Name).ToList()
			});
		}

		public ActionResult Html() {
			var data = BuildEndpointData();
			return View(new EndpointData {
				Areas = data.Select(a => a.Value).OrderBy(a => a.Name).ToList()
			});
		}


		/// <summary>
		/// Builds the endpoints data.
		/// </summary>
		/// <returns></returns>
		private Dictionary<String, EndpointArea> BuildEndpointData() {
			var areaRoute = (String)RouteData.Values["area"];
			var areas = new Dictionary<String, EndpointArea>();

			var reflector = new Reflector();
			var types = reflector.GetTypes(AppDomain.CurrentDomain);

			foreach(var type in reflector.GetTypes(AppDomain.CurrentDomain)) {
				var areaName = reflector.FindAreaFromNamespace(type.Namespace);
				var currentArea = new EndpointArea {
					Name = areaName,
					QualifiedName = areaName
				};

				if(areas.ContainsKey(areaName)) {
					currentArea = areas[areaName];
				}

				if(!String.IsNullOrWhiteSpace(areaRoute) && String.Compare(areaName, areaRoute, true) != 0) {
					continue;
				}

				if(!areas.ContainsKey(areaName)) {
					areas.Add(areaName, currentArea);
				}

				var deprecated = type.GetCustomAttribute<DeprecatedAttribute>();
				var obsolete = type.GetCustomAttribute<ObsoleteAttribute>();
				var sinceVer = type.GetCustomAttribute<SinceVersionAttribute>();
				var auth = type.HasAttribute<RequiresAuthenticationAttribute>() || type.HasAttribute<AuthorizeAttribute>();
				var reqHttps = type.HasAttribute<RequireHttpsAttribute>();

				var endpoint = type.GetCustomAttribute<EndpointAttribute>();
				var serviceContract = type.GetCustomAttribute<ServiceContractAttribute>();

				// create the service for the type
				var epService = new EndpointService {
					Properties = reflector.GetCustomProperties(type).ToList(),
					Gists = reflector.GetGists(type).ToList()
				};


				epService.Name = reflector.GetName(type);
				epService.Description = reflector.GetDescription(type);
				epService.QualifiedName = "{0}.{1}".With(areaName, epService.Name);

				// get all the applicable methods for the type
				foreach(var method in reflector.GetMethods(type)) {
					var name = reflector.GetName(method);
					var returnType = reflector.GetReturnType(method);

					var scheme = reflector.DoesRequireHttps(method) ? "https" : Request.Url.Scheme;
					var urlParams = GenerateUrlParams(method);
					urlParams.Add("area", areaName.ToLower());
					var actionUrl = GenerateActionUrl(epService.Name.ToLower(), name.ToLower(), urlParams, scheme);
					var methods = reflector.GetHttpVerbs(method);
					var qualifiedKey = "{0}.{1}.{2}_{3}".With(areaName, epService.Name, name, String.Join("_", methods));
					var epi = new EndpointInfo() {
						IsSystemType = returnType.GetUnderlyingType().IsSystemType(),
						QualifiedName = "{0}.{1}.{2}".With(areaName, epService.Name, name),
						Name = name,
						ReturnType = returnType.ToArrayName(),
						QualifiedReturnType = returnType.QualifiedName(),
						QualifiedUnderlyingReturnType = returnType.GetUnderlyingType().QualifiedName().Replace("[]", ""),
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
						epService.Endpoints.Add(qualifiedKey, epi);
					} catch(ArgumentException aex) {
						throw new ArgumentException("{0}: {1} {2}".With(aex.Message, qualifiedKey, type.FullName));
					}
				}
				currentArea.Services.Add(epService.Name, epService);
			}
			return areas;
		}

		private IDictionary GenerateUrlParams(System.Reflection.MethodInfo method) {
			var mparams = method.GetParameters();
			var result = new Dictionary<string, object>();
			mparams.ForEach(p => {
				var name = p.GetCustomAttributeValue<DisplayAttribute, string>(x => x.Name).Or(p.Name);
				var sample = p.GetCustomAttributeValue<SampleValueAttribute, Object>(x => x.Value).Or(p.DefaultValue.Or(""));
				result.Add(name, sample.ToString());
			});
			return result;
		}

		/// <summary>
		/// Generates the action URL.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="action">The action.</param>
		/// <param name="routeValues">The route values.</param>
		/// <returns></returns>
		private String GenerateActionUrl(string controller, string action, IDictionary routeValues, String scheme) {

			RouteValueDictionary rvd = new RouteValueDictionary((Dictionary<string, object>)routeValues);

			return Url.Action(action, controller, rvd, scheme);
		}


	}
}
