using System.Web;
using System.Web.Optimization;
using Camalot.Common.Extensions;
using Camalot.Common.Mvc.Extensions;
using EndpointMvc.Extensions;

namespace EndpointMvc.Site {

	public class BundleConfig {
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles ( BundleCollection bundles ) {
			bundles.LoadFromWebConfiguration ( );

			bundles.AddEndpointMvcScripts ( "~/assets/scripts" );
			bundles.AddEndpointMvcStyles ( "~/assets/styles/" );
		}
	}
}
