using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;
using Camalot.Common.Extensions;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {
		public static void AddEndpointMvcScripts ( this BundleCollection bundles ) {
			AddEndpointMvcScripts ( bundles, "~/Scripts/" );
		}
		public static void AddEndpointMvcScripts ( this BundleCollection bundles, string path ) {
			if(path.IsMatch("/$")) {
				path = path.Substring(0,path.Length -1);
			}
			bundles.Add ( new ScriptBundle ( "~/bundles/endpointmvc/js" )
				.Include ( "{0}/endpointmvc.js".With ( path ) ) 
			);
		}

		public static void AddEndpointMvcStyles ( this BundleCollection bundles ) {
			AddEndpointMvcStyles ( bundles, "~/Content/" );
		}
		public static void AddEndpointMvcStyles ( this BundleCollection bundles, string path ) {
			if ( path.IsMatch ( "/$" ) ) {
				path = path.Substring ( 0, path.Length - 1 );
			}
			bundles.Add ( new ScriptBundle ( "~/bundles/endpointmvc/css" )
				.Include ( "{0}/endpointmvc.css".With ( path ) )
			);
		}
	}
}
