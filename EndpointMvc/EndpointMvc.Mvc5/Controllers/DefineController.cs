using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EndpointMvc.Models;
using EndpointMvc.Reflection;
using EndpointMvc.Extensions;
using System.Reflection;
using EndpointMvc.Attributes;
using Camalot.Common.Extensions;
using Camalot.Common.Mvc.Results;

namespace EndpointMvc.Controllers {
	public class DefineController : Controller{

		public JsonResult Json ( String id ) {
			throw new NotImplementedException ( );
		}

		public ActionResult Xml ( String id ) {
			throw new NotImplementedException ( );
		}

		public ActionResult Html ( String id ) {
			var model = GetDefineParamInfo ( id.Require ( ) );

			// the view lives in endpoints
			return View ( "Define", model );
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

				if ( type.Is<Enum> ( ) ) {
					data.Fields.AddRange ( reflector.GetEnumInfo ( type ) );
				} else {
					var props = type.GetProperties ( BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public ).Where ( p => !p.HasAttribute<IgnoreAttribute> ( ) );
					foreach ( var prop in props ) {
						data.Properties.AddRange ( reflector.GetPropertyInfo ( "", prop ) );
					}
				}
				return data;
			}
			return null;
		}


	}
}
